namespace ElsuGiyim.Application.Auth;

public record LoginRequestDto(string Email, string Password);

public record LoginResponseDto(
    string Token,
    DateTime ExpiresAtUtc,
    string FullName,
    string Email,
    IReadOnlyList<string> Roles
);

public record CurrentUserDto(
    string Email,
    string FullName,
    IReadOnlyList<string> Roles
);
