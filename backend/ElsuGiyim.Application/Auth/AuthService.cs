using ElsuGiyim.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElsuGiyim.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var user = await _context.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);

        // Kullanıcı yoksa veya pasifse ya da şifre uyuşmuyorsa aynı genel
        // sonucu (null) döneriz — bu, saldırganın "bu e-posta kayıtlı mı"
        // bilgisini öğrenmesini (user enumeration) engeller.
        if (user is null || !user.IsActive) return null;
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash)) return null;

        var roleNames = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        var token = _jwtTokenGenerator.GenerateToken(user, roleNames, out var expiresAtUtc);

        return new LoginResponseDto(token, expiresAtUtc, user.FullName, user.Email, roleNames);
    }
}
