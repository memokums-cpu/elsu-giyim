using System.Security.Claims;
using ElsuGiyim.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElsuGiyim.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>E-posta ve şifre ile giriş yapar, başarılıysa JWT döner.</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> Login(
        [FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        if (result is null)
        {
            // Kasıtlı olarak genel bir mesaj: e-posta mı şifre mi yanlış
            // olduğunu belirtmiyoruz (user enumeration saldırılarına karşı).
            return Unauthorized(new { message = "E-posta veya şifre hatalı." });
        }

        return Ok(result);
    }

    /// <summary>Geçerli JWT'nin ait olduğu kullanıcının bilgilerini döner.</summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
    public ActionResult<CurrentUserDto> Me()
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var fullName = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        return Ok(new CurrentUserDto(email, fullName, roles));
    }
}
