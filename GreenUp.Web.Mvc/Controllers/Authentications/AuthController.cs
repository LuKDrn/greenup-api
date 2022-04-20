using GreenUp.Application.Authentications.Tokens;
using GreenUp.Application.Users;
using GreenUp.Application.Users.Dtos;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.Core.Business.Addresses.Models;
using GreenUp.Core.Business.Users.Models;
using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Models.Auth;
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

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : GreenUpControllerBase
    {
        protected readonly IUserAppService _userAppService;
        protected readonly ITokenService _tokenService;
        public AuthController(GreenUpContext _context, IConfiguration _config, IUserAppService userAppService, ITokenService tokenService)
            : base(_context, _config)
        {
            _userAppService = userAppService;
            _tokenService = tokenService;
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
            if (user != null)
            {

                if (user.IsEmailConfirmed)
                {
                    return BadRequest($"L'adresse {user.Mail} est déjà confirmée.");
                }
                user.IsEmailConfirmed = true;
                await _context.SaveChangesAsync();
                return Ok($"L'adresse {user.Mail} a été confirmée.");
            }
            return NotFound("Aucune validation confirmée, aucun utilisateur n'a pu être trouvé");
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
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

        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<string>> SignUp([FromBody] SignUpUserModel model)
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
                    await SendConfirmMail(user.Id.ToString());
                    return $"Le compte utilisateur {user.Mail} a été crée. Veuillez confirmer votre adresse mail.";
                }
                return BadRequest("Cette adresse mail est déjà utilisé.");
            }
            return Unauthorized("Les informations renseignées ne permettent de finaliser l'inscription.");
        }
    }
}
