using GreenUp.Application.Authentications.Tokens;
using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Associations;
using GreenUp.Web.Mvc.Models.Missions;
using GreenUp.Web.Mvc.Models.Participations;
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
        public AssociationsController(GreenUpContext _context, IConfiguration _config, ITokenService tokenService) : base(_context, _config)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User association = await _context.Users.FirstOrDefaultAsync(u => u.RnaNumber == model.RnaNumber && u.IsAssociation);
                if (association != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, association.Password);
                    if (verified)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, association.LastName),
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
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpAssociationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User association = new()
                {
                    CreationTime = DateTime.Now,
                    LastName = model.Name,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RnaNumber = model.RnaNumber,
                    PhoneNumber = model.PhoneNumber,
                    WebsiteUrl = model.WebsiteUrl,
                    Mail = model.Mail,
                    Addresses = new List<Address>(),
                    IsAssociation = true
                };
                var headquarter = new Address()
                {
                    Place = model.Adress,
                    City = model.City,
                    ZipCode = model.ZipCode
                };
                association.Addresses.Add(headquarter);
                _context.Users.Add(association);
                await _context.SaveChangesAsync();
                return association;
            }
            return Ok(new { Error = "Model not valid" });
        }

        [Authorize]
        [HttpGet, Route("Dashboard/{id}")]
        public async Task<ActionResult<OneAssociationViewModel>> Dashboard(Guid id)
        {
            User association = await GetOneAssociation(id, true).FirstOrDefaultAsync();
            if (association != null)
            {
                OneAssociationViewModel model = new()
                {
                    Id = association.Id,
                    CreationTime = association.CreationTime,
                    Name = association.LastName,
                    RnaNumber = association.RnaNumber,
                    Logo = association.Photo,
                    PhoneNumber = association.PhoneNumber,
                    Mail = association.Mail,
                    IsActive = association.IsActive,
                    Addresses = association.Addresses,
                };
                foreach (var mission in association.Missions)
                {
                    var participants = new List<OneParticipantViewModel>();
                    foreach (var user in mission.Participants)
                    {
                        participants.Add(ConvertUserToParticipantViewModel(user));
                    }
                    var missions = new List<OneMissionViewModel>();
                    missions.Add(ConvertMissionToViewModel(mission, participants));

                }

                return model;
            }
            return null;
        }

        private OneParticipantViewModel ConvertUserToParticipantViewModel(Participation user)
        {
            return new OneParticipantViewModel
            {
                UserId = user.UserId,
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                Mail = user.User.Mail,
                DateInscription = user.DateInscription.ToString("dd/MM/yyyy HH:mm"),
                Photo = user.User.Photo,
            };
        }

        private OneMissionViewModel ConvertMissionToViewModel(Mission mission, ICollection<OneParticipantViewModel> participants)
        {
            return new OneMissionViewModel
            {
                Id = mission.Id,
                Titre = mission.Title,
                Description = mission.Description,
                Start = mission.Start.ToString("dd/MM/yyyy HH:mm"),
                End = mission.End.ToString("dd/MM/yyyy HH:mm"),
                Creation = mission.Creation.ToString("dd/MM/yyyy HH:mm"),
                Edit = mission.Edit.ToString("dd/MM/yyyy HH:mm"),
                IsInGroup = mission.IsInGroup,
                NumberPlaces = mission.NumberPlaces,
                RewardValue = mission.RewardValue,
                Status = new Status
                {
                    Id = mission.StatusId,
                    Value = mission.Status.Value
                },
                Address = new Address
                {
                    Id = mission.LocationId,
                    Place = mission.Location.Place,
                    City = mission.Location.City,
                    ZipCode = mission.Location.ZipCode
                },
                Tasks = mission.Tasks,
                Participants = participants,
            };
        }
    }
}
