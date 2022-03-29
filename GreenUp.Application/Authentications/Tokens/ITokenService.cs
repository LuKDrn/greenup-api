using System.Collections.Generic;
using System.Security.Claims;

namespace GreenUp.Application.Authentications.Tokens
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
