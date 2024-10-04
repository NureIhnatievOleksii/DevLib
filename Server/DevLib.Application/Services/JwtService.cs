using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DevLib.Application.Interfaces.Services;
using DevLib.Application.Options;
using DevLib.Domain.UserAggregate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevLib.Application.Services;

public class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{
    public Task<string> GenerateJwtTokenAsync(string userName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(jwtOptions.Value.ExpireDays),
            signingCredentials: creds);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
