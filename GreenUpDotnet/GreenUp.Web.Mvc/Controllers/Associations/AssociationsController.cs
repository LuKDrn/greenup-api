using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Classes;
using GreenUp.Web.Mvc.Models.Associations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Associations
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationsController : GreenUpControllerBase
    {
        protected readonly ITokenService _tokenService;
        public AssociationsController(GreenUpContext _context, IConfiguration _config,ITokenService tokenService) : base(_context, _config)
        {
            _tokenService = tokenService;
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Association association = await _context.Associations.FirstOrDefaultAsync(u =>u.Name == model.Name  && u.Siren == model.Siren);
                if (association != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, association.Password);
                    if (verified)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, association.Name),
                            new Claim(ClaimTypes.Role, "Association")
                        };
                        var accessToken = _tokenService.GenerateAccessToken(claims);
                        var refreshToken = _tokenService.GenerateRefreshToken();
                        association.RefreshToken = refreshToken;
                        association.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                        await _context.SaveChangesAsync();
                        return Ok(new
                        {
                            Token = accessToken,
                            RefreshToken = refreshToken
                        });
                    }
                    else
                    {
                        return Unauthorized("Informations incorrectes");
                    }
                }
                else
                {
                    return Unauthorized("Informations incorrectes");
                }
            }
            else
            {
                return BadRequest("Model not valid");
            }
        }

        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<Association>> SignUp([FromBody]SignUpAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _context.Roles.Include(r => r.Assocations).FirstOrDefaultAsync(x => x.Value == "Association");
                Association association = new Association()
                {
                    Name = model.Name,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Siren = model.Siren,
                    Role = role,
                    Adress = new Adress()
                    {
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = model.ZipCode
                    }
                };
                role.Assocations.Add(association);
                await _context.SaveChangesAsync();
                return association;
            }
            return Ok(new { Error = "Model not valid" });
        }
    }
}
