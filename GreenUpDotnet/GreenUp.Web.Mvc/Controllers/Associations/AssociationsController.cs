using GreenUp.Application.Authentications.Tokens;
using GreenUp.Core.Business.Adresses.Models;
using GreenUp.Core.Business.Associations.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Adresses;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Associations
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationsController : GreenUpControllerBase
    {
        protected readonly ITokenService _tokenService;
        public AssociationsController(GreenUpContext _context, IConfiguration _config,ITokenService tokenService) : base(_context, _config)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Association association = await _context.Associations.FirstOrDefaultAsync(u => u.Name == model.Name  && u.Siren == model.Siren);
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

        [AllowAnonymous]
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<Association>> SignUp([FromBody]SignUpAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Association association = new()
                {
                    Name = model.Name,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Siren = model.Siren,
                    PhoneNumber = model.PhoneNumber,
                    Website = model.Website,
                    Mail = model.Mail,
                    Adress = new Adress()
                    {
                        Place = model.Adress,
                        City = model.City,
                        ZipCode = model.ZipCode
                    }
                };
                _context.Associations.Add(association);
                await _context.SaveChangesAsync();
                return association;
            }
            return Ok(new { Error = "Model not valid" });
        }

        [Authorize]
        [HttpGet, Route("Dashboard/{id}")]
        public async Task<ActionResult<OneAssociationViewModel>> Dashboard(Guid id)
        {
            Association association = await GetOneAssociation(id, true).FirstOrDefaultAsync();
            if(association != null)
            {
                var associationMissions = new List<OneMissionViewModel>();
                if(association.Missions.Count > 0)
                {
                    foreach (var mission in association.Missions)
                    {
                        var missionUsers = new List<OneMissionUserViewModel>();
                        if(mission.Users.Count > 0)
                        {
                            foreach (var user in mission.Users)
                            {
                                missionUsers.Add(new OneMissionUserViewModel
                                {
                                    Id = user.UserId,
                                    FirstName = user.User.FirstName,
                                    LastName = user.User.LastName,
                                    Mail = user.User.Mail,
                                    DateInscription = user.DateInscription.ToString("dd/MM/yyyy HH:mm"),
                                    Photo = user.User.Photo,
                                });
                            }
                        }
                        associationMissions.Add(new OneMissionViewModel
                        {
                            Id = mission.Id,
                            Titre = mission.Titre,
                            Description = mission.Description,
                            Date = mission.Date.ToString("dd/MM/yyyy HH:mm"),
                            Available = mission.Available,
                            IsInGroup = mission.IsInGroup,
                            NumberPlaces = mission.NumberPlaces,
                            RewardValue = mission.RewardValue,
                            Adress = new OneAdressViewModel
                            {
                                Id = mission.LocationId,
                                Place = mission.Location.Place,
                                City = mission.Location.City,
                                ZipCode = mission.Location.ZipCode
                            },
                            AssociationId = id.ToString(),
                            Users = missionUsers
                        });
                    }
                }
                OneAssociationViewModel model = new()
                {
                    Id = association.Id,
                    Name = association.Name,
                    Siren = association.Siren.ToString(),
                    Logo = association.Logo,
                    Missions = associationMissions
                };
                return model;
            }
            return null;                       
        }
    }
}
