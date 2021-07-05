using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : GreenUpControllerBase
    {
        public AuthController(GreenUpContext context, IConfiguration config) : base(context, config)
        {}

        [HttpPost, Route("login")]
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
                        var tokeOptions = new JwtSecurityToken(
                            issuer: "https://localhost:5001",
                            audience: "https://localhost:5001",
                            claims: new List<Claim>(),
                            expires: DateTime.Now.AddMinutes(5),
                            signingCredentials: signinCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                        return Ok(new { Token = tokenString });
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

        [HttpPost, Route("register")]
        public async Task<User> Register([FromBody]RegisterViewModel model)
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
            throw new Exception("Model not valid");
        }
    }
}
