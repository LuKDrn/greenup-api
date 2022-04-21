using GreenUp.Application.Authentications.Tokens;
using GreenUp.Application.Users;
using GreenUp.Application.Users.Dtos;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.Core.Business.Addresses.Models;
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
        protected readonly IUserAppService _userAppService;
        public UsersController(GreenUpContext context, IConfiguration config, IUserAppService userAppService) : base(context, config)
        {
            _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost, Route("[action]")]
        public IActionResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {

            GetAllUsersInput input = new()
            {
                Message = model.Message
            };
            input.Users.Add(new UserDataForMail
            {
                Email = model.Mail,
                FirstName = model.Name,
                PhoneNumber = model.Phone,
            });            
            _userAppService.ContactGreenUp(input);
            return Ok("Votre message à bien été envoyé.");
            }
            return BadRequest("Les informations saisies ne permettent pas l'envoi du message");
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<OneUserViewModel>> Account(string id)
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
            return NotFound("Aucun utilisateur trouvé" );
        }

        [HttpPut, Route("EditAccount")]
        public async Task<ActionResult<User>> EditProfile([FromBody] OneUserViewModel model)
        {
            var user = await GetUser(model.Id, false).Include(u => u.Addresses).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Mail = model.Mail;
                user.PhoneNumber = model.PhoneNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Photo = UploadImage(model.NewPhoto);
                user.Birthdate = Convert.ToDateTime(model.Birthdate);
                user.Addresses.Clear();
                _context.Adresses.RemoveRange(user.Addresses);
                user.Addresses.Add(new Address
                {
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Place = model.Place,
                    UserId = new Guid(model.Id)
                });
                await _context.SaveChangesAsync();
                return user;
            }
            return NotFound("Aucun utilisateur trouvé");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery]string id)
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
                return Ok($"Votre compte a été supprimé");
            }
            return NotFound("Aucun utilisateur trouvé");
        }

    }
}
