using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FTS.Application.DTO;
using FTS.Application.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FTS.Infrastructure.Auth;

internal sealed class Authenticator(IOptions<AuthOptions> options) : IAuthenticator
{
    public JwtDto CreateToken(Guid userId, string role)
    {
        JwtSecurityTokenHandler jwtSecurityToken = new JwtSecurityTokenHandler();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = DateTime.UtcNow.Add(options.Value.Expiry ?? TimeSpan.FromHours(1));
        var jwt = new JwtSecurityToken(issuer: options.Value.Issuer, audience: options.Value.Audience, claims: claims, expires: expires, signingCredentials: signingCredentials);
        var token = jwtSecurityToken.WriteToken(jwt);
        return new JwtDto(token);
    }
}
