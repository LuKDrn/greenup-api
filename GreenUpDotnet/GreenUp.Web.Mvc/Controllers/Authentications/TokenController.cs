using GreenUp.EntityFrameworkCore.Data;
using GreenUp.Web.Core.Controllers;
using GreenUp.Web.Mvc.Classes;
using GreenUp.Web.Mvc.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
        [Route("api/[controller]")]
        [ApiController]
    public class TokenController : GreenUpControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(GreenUpContext context, IConfiguration config, ITokenService tokenService) : base(context, config)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenApiModel model)
        {
            if (model is null)
            {
                return BadRequest("Invalid client request");
            }
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Mail == principal.Identity.Name);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await  _context.SaveChangesAsync();
            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Mail == User.Identity.Name);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
