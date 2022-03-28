using GreenUp.Application.Users;
using GreenUp.Application.Users.Dtos;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : GreenUpControllerBase
    {
        protected readonly IUserAppService _userAppService;
        public AuthController(GreenUpContext _context, IConfiguration _config, IUserAppService userAppService) 
            : base (_context, _config)
        {
            _userAppService = userAppService;
        }
        
        [HttpPost, Route("SendConfirmMail")]
        public async Task SendConfirmMail(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => id == u.Id.ToString());
            if (user.IsEmailConfirmed)
            {
                throw new Exception("Adresse mail déjà confirmé.");
            }
            else
            {
                GetAllUsersInput input = new();
                input.Users.Add(new UserDataForMail
                {
                    Email = user.Mail,
                    UserId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });
                _userAppService.ConfirmAccount(input);
            }
        }

        [HttpPost, Route("UpdateConfirmMail")]
        public async Task<IActionResult> UpdateConfirmMail([FromBody]string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => id == u.Id.ToString());
            user.IsEmailConfirmed = true;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Success = "L'adresse mail a été confirmé",
                User = user
            });
        }
    }
}
