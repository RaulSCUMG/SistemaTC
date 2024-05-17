using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SistemaTC.Core;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaTC.Services;
public class TokenService(AppSettings appSettings, ILogger<TokenService> logger) : ITokenService
{
    // Specify how long until the token expires
    private const int ExpirationMinutes = 30;

    public string CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();

        logger.LogInformation("JWT Token created");

        return tokenHandler.WriteToken(token);
    }

    private static JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
    DateTime expiration) =>
    new(
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
    );

    private List<Claim> CreateClaims(User user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, appSettings.JwtTokenSettings.JwtRegisteredClaimNamesSub),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.RoleId.ToString())
            };

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appSettings.JwtTokenSettings.SymmetricSecurityKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}
