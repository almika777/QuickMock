using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenService(IConfiguration configuration)
{
    private readonly byte[] _key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
    private const int AccessTokenExpirationDays = 3;
    private const int RefreshTokenExpirationDays = 9;

    private static readonly JwtSecurityTokenHandler Handler = new();

    public string GenerateAccessToken(UserLoginModel userModel)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Email, userModel.Email)
            ]),
            Expires = DateTime.UtcNow.AddDays(AccessTokenExpirationDays),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = Handler.CreateToken(tokenDescriptor);
        return Handler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = Handler.CreateToken(tokenDescriptor);
        return Handler.WriteToken(token);
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        var jwtToken = Handler.ReadJwtToken(refreshToken);

        if (jwtToken.Payload.Expiration == null)
            return false;
        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(jwtToken.Payload.Expiration.Value).UtcDateTime;

        return DateTime.UtcNow > expirationTime;
    }
}