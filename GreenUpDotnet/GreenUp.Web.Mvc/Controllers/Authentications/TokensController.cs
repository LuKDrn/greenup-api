using GreenUp.Application.Authentications.Tokens;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Web.Mvc.Controllers.Authentications
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : GreenUpControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokensController(GreenUpContext context, IConfiguration config, ITokenService tokenService) : base(context, config)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateAdminToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);


            await _context.SaveChangesAsync();
            return Ok(new
            {
                Token = accessToken
            });
        }

        [HttpPost]
        [Route("refresh")]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
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
