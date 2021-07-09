using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Classes;
using GreenUp.Web.Mvc.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : GreenUpControllerBase 
    {
        private readonly ITokenService _tokenService;
        public AuthController(GreenUpContext context, IConfiguration config, ITokenService tokenService) : base(context, config)
        {
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserViewModel>> Account(Guid id)
        {
            User user = await GetUser(id, false)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                UserViewModel model = new UserViewModel()
                {
                    Id = id,
                    Password = user.Password,
                    Mail = user.Mail,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Photo = user.Photo,
                    Points = user.Points,
                    Role = user.Role.Value,
                    Adress = user.Adress,
                    Missions = user.Missions
                };
                return model;
            }
            else
            {
                return Ok(new { Error = "Aucun utilisateur trouvé" });
            }
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Mail == model.Mail);
                if (user != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                    if (verified)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Mail),
                            new Claim(ClaimTypes.Role, "User")
                        };
                        var accessToken = _tokenService.GenerateAccessToken(claims);
                        var refreshToken = _tokenService.GenerateRefreshToken();
                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                        await _context.SaveChangesAsync();
                        return Ok(new
                        {
                            Token = accessToken,
                            RefreshToken = refreshToken
                        });
                    }
                    else
                    {
                        return Unauthorized("Mail ou Mot de passe incorrecte");
                    }
                }
                else
                {
                    return Unauthorized("Mail ou mot de passe incorrecte.");
                }
            }
            else
            {
                return BadRequest("Model not valid");
            }
        }

        [HttpPost, Route("Register")]
        public async Task<ActionResult<User>> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Mail = model.Mail,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    Role = await _context.Roles.Where(r => r.Value == "User").FirstOrDefaultAsync()
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
          return Ok(new { Error = "Model not valid" });
        }

        [HttpPut, Route("EditProfile")]
        public async Task<ActionResult<User>> EditProfile(UserViewModel model)
        {
            var user = await GetUser(model.Id, false).FirstOrDefaultAsync();
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Photo = model.Photo;
                user.BirthDate = model.BirthDate;

                return user;
            }
            else
            {
                return Ok("Aucun utilisateur trouvé");
            }
        }

        [HttpPut, Route("EditMail")]
        public async Task<string> EditMail(UserViewModel model)
        {
            var user = await GetUser(model.Id, false).FirstOrDefaultAsync();
            if(user != null)
            {
                user.Mail = model.Mail;

                return user.Mail;
            }
            else
            {
                return "Aucun utilisateur à modifié";
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetUser(id, true).FirstOrDefaultAsync();
            if(user!= null)
            {
                foreach (var mission in user.Missions)
                {
                    mission.Users.Remove(user);
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(new { Success = $"Votre compte à été supprimé" });
            }
            return Ok(new { Error = "Aucun utilisateur trouvé" });
        } 
    }
}
