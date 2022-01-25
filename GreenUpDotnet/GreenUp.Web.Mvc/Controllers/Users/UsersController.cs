using GreenUp.Application.Authentications.Tokens;
using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Associations;
using GreenUp.Web.Mvc.Models.Missions;
using GreenUp.Web.Mvc.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : GreenUpControllerBase
    {
        private readonly ITokenService _tokenService;
        public UsersController(GreenUpContext context, IConfiguration config, ITokenService tokenService) : base(context, config)
        {
            _tokenService = tokenService;
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<OneUserViewModel>> Account(Guid id)
        {
            User user = await GetUser(id, true).FirstOrDefaultAsync();
            if (user != null)
            {                           
                OneUserViewModel model = new()
                {
                    Id = id,
                    Password = user.Password,
                    Mail = user.Mail,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birthdate = user.Birthdate.ToString("dd/MM/yyyy HH:mm"),
                    Photo = user.Photo,
                    Points = user.Points,
                    Adresses = user.Addresses,
                    Participations = user.Participations,
                    Favorites = user.Favorites,
                    Orders = user.Orders
                };
                return model;
            }
                return Ok(new { Error = "Aucun utilisateur trouvé" });
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsUser && u.Mail == model.Mail);
                if (user != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                    if (verified)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                        {
                            new Claim("type", "User"),
                            new Claim("userId", user.Id.ToString()),
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

        [AllowAnonymous]
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<string>> SignUp([FromBody] SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool alreadyUserWithMail = await _context.Users.Where(u => u.Mail == model.Mail).AnyAsync();
                if (!alreadyUserWithMail)
                {
                    User user = new()
                    {     
                        IsUser = true,
                        Mail = model.Mail,
                        PhoneNumber = model.PhoneNumber,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Birthdate = model.Birthdate,
                        CreationTime = DateTime.Now,
                        Addresses = new List<Address>()
                    };
                    user.Addresses.Add(new Address()
                    {
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = model.ZipCode
                    });
                    user.Photo = UploadImage(model.Photo);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return $"The user {model.Mail} has been created";
                }
                    return Ok(new { Error = "Cette adresse mail est déjà utilisé." });
            }
            return Ok(new { Error = "Les informations renseignées ne permettent de finaliser l'inscription." });
        }

        [HttpPut, Route("EditAccount")]
        public async Task<ActionResult<User>> EditProfile([FromBody] OneUserViewModel model)
        {
            var user = await GetUser(model.Id, false).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Mail = model.Mail;
                user.PhoneNumber = model.PhoneNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Photo = UploadImage(model.NewPhoto);
                user.Birthdate = Convert.ToDateTime("model.BirthDate");
                return user;
            }
            return Ok("Aucun utilisateur trouvé");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetUser(id, true).FirstOrDefaultAsync();
            if (user != null)
            {
                foreach (var participation in user.Participations)
                {
                    _context.Participations.Remove(participation);
                };
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(new { Success = $"Votre compte a été supprimé" });
            }
            return Ok(new { Error = "Aucun utilisateur trouvé" });
        }

        [HttpPost, Route("Inscription")]
        public async Task<ActionResult> Inscription([FromQuery]int missionId, Guid userId)
        {
            Mission mission = await GetOneMission(missionId, false).Include(m => m.Participants).FirstOrDefaultAsync();
            User user = await GetUser(userId, false).Include(u => u.Missions).FirstOrDefaultAsync();
            if (mission == null)
            {
                return NotFound($"Aucune mission n'a été trouvé.");
            }          
            else
            {
                return Ok(new { error = $"Cette n'enregistre plus de nouvelles inscriptions" });
            }
        }
    }
}
