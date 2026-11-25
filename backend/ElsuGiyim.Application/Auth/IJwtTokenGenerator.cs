using ElsuGiyim.Domain.Entities;

namespace ElsuGiyim.Application.Auth;

public interface IJwtTokenGenerator
{
    /// <summary>Verilen kullanıcı ve rolleri için imzalı bir JWT üretir.</summary>
    string GenerateToken(User user, IReadOnlyList<string> roleNames, out DateTime expiresAtUtc);
}
