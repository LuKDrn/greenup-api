using GreenUp.Application.Authentications.Tokens;
using GreenUp.Core.Business.Missions.Models;
using GreenUp.Core.Business.Participations.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : GreenUpControllerBase
    {
        public UsersController(GreenUpContext context, IConfiguration config) : base(context, config)
        {
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
                    IsUser = user.IsUser,
                    IsAdmin = user.IsAdmin,
                    IsActive = user.IsActive, 
                    IsAssociation = user.IsAssociation,
                    IsCompany = user.IsCompany,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsPhoneNumberConfirmed = user.IsPhoneNumberConfirmed,
                    RnaNumber = user.RnaNumber,
                    SiretNumber = user.SiretNumber,
                    WebsiteUrl = user.WebsiteUrl,
                    Adresses = user.Addresses,
                    Participations = user.Participations,
                    Favorites = user.Favorites,
                    Orders = user.Orders
                };
                return model;
            }
                return Ok(new { Error = "Aucun utilisateur trouvé" });
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
            else if(mission.Participants.Count == mission.NumberPlaces)
            {
                return Ok(new { error = $"Cette n'enregistre plus de nouvelles inscriptions" });
            }
            else
            {
                mission.Participants.Add(new Participation
                {
                    MissionId = missionId,
                    UserId = userId,
                    DateInscription = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                return Ok(new { success = $"Participation enregistrée" });
            }
        }
    }
}
