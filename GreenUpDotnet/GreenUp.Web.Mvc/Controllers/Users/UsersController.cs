using GreenUp.Application.Authentications.Tokens;
using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Adresses;
using GreenUp.Web.Mvc.Models.Associations;
using GreenUp.Web.Mvc.Models.Missions;
using GreenUp.Web.Mvc.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public async Task<ActionResult<OneUserViewModel>> Account(string id)
        {
            User user = await GetUser(Guid.Parse(id), false).Include(u => u.Role).Include(u => u.Adress).Include(u => u.Missions).FirstOrDefaultAsync();
            if (user != null)
            {
                var userMissions = new List<OneMissionViewModel>();
                if (user.Missions.Count > 0)
                {
                    foreach (var mission in user.Missions)
                    {
                        userMissions.Add(new OneMissionViewModel
                        {
                            Id = mission.MissionId,
                            Titre = mission.Mission.Titre,
                            Description = mission.Mission.Description,
                            Date = mission.Mission.Date.ToString("dd/MM/yyyy HH:mm"),
                            Available = mission.Mission.Available,
                            IsInGroup = mission.Mission.IsInGroup,
                            RewardValue = mission.Mission.RewardValue,
                            NumberPlaces = mission.Mission.NumberPlaces,
                            Adress = new OneAdressViewModel
                            {
                                Id = mission.Mission.LocationId,
                                City = mission.Mission.Location.City,
                                Place = mission.Mission.Location.Place,
                                ZipCode = mission.Mission.Location.ZipCode
                            },
                            Association = new OneAssociationViewModel
                            {
                                Id = mission.Mission.AssociationId,
                                Name = mission.Mission.Association.Name,
                                Siren = mission.Mission.Association.Siren.ToString(),
                                Logo = mission.Mission.Association.Logo
                            }
                        });
                    }
                }
                OneUserViewModel model = new()
                {
                    Id = Guid.Parse(id),
                    Password = user.Password,
                    Mail = user.Mail,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate.ToString("dd/MM/yyyy HH:mm"),
                    Photo = user.Photo,
                    Points = user.Points,
                    Role = user.Role.Value,
                    Adress = new OneAdressViewModel
                    {
                        Id = user.AdressId,
                        Place = user.Adress.Place,
                        City = user.Adress.City,
                        ZipCode = user.Adress.ZipCode
                    },
                    Missions = userMissions
                };
                return model;
            }
            else
            {
                return Ok(new { Error = "Aucun utilisateur trouvé" });
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
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
                            new Claim("type", "User"),
                            new Claim("id", user.Id.ToString()),
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
                var role = await _context.Roles.Include(r => r.Users).Where(r => r.Value == "User").FirstOrDefaultAsync();
                bool alreadyUserWithMail = await _context.Users.Where(u => u.Mail == model.Mail).AnyAsync();
                if (!alreadyUserWithMail)
                {
                    User user = new()
                    {
                        Mail = model.Mail,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        CreationTime = DateTime.Now,
                        Role = role
                    };
                    user.Adress = new Adress()
                    {
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = model.ZipCode
                    };
                    user.Photo = model.Photo;//UploadImage(model.Photo);
                    role.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return $"The user {model.Mail} has been created";
                }
                else
                {
                    return Ok(new { Error = "Cette adresse mail est déjà utilisé." });
                }
            }
            return Ok(new { Error = "Les informations renseignées ne permettent de finaliser l'inscription." });
        }

        [HttpPut, Route("EditAccount")]
        public async Task<ActionResult<User>> EditProfile([FromBody] OneUserViewModel model)
        {
            var user = await GetUser(model.Id, false).FirstOrDefaultAsync();
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Photo = UploadImage(model.NewPhoto);
                user.BirthDate = Convert.ToDateTime("model.BirthDate");             
                return user;
            }
            else
            {
                return Ok("Aucun utilisateur trouvé");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetUser(id, true).FirstOrDefaultAsync();
            if (user != null)
            {
                foreach (var missionUser in user.Missions)
                {
                    _context.MissionUsers.Remove(missionUser);
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
            Mission mission = await _context.Missions.Include(m => m.Users).FirstOrDefaultAsync(m => m.Id == missionId);
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
