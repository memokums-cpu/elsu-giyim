using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElsuGiyim.Application.Auth;
using ElsuGiyim.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ElsuGiyim.Infrastructure.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _signingKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _signingKey = configuration["Jwt:SigningKey"]
            ?? throw new InvalidOperationException(
                "Jwt:SigningKey bulunamadı. User Secrets ile tanımlandığından emin olun.");
        _issuer = configuration["Jwt:Issuer"] ?? "ElsuGiyim.API";
        _audience = configuration["Jwt:Audience"] ?? "ElsuGiyim.Client";
        _expiryMinutes = configuration.GetValue<int?>("Jwt:ExpiryMinutes") ?? 60;
    }

    public string GenerateToken(User user, IReadOnlyList<string> roleNames, out DateTime expiresAtUtc)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, user.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        claims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        expiresAtUtc = DateTime.UtcNow.AddMinutes(_expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
