namespace ElsuGiyim.Application.Auth;

public interface IAuthService
{
    /// <summary>
    /// E-posta ve şifreyi doğrular, başarılıysa JWT döner.
    /// Başarısızsa null döner (hangi kısmın yanlış olduğu — e-posta mı
    /// şifre mi — güvenlik gereği belirtilmez).
    /// </summary>
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
