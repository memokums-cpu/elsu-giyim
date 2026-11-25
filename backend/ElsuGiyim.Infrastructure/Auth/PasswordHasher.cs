using System.Security.Cryptography;
using ElsuGiyim.Application.Auth;

namespace ElsuGiyim.Infrastructure.Auth;

/// <summary>
/// PBKDF2 (RFC 2898) ile şifre hashleme. Harici paket gerektirmez,
/// .NET'in kendi System.Security.Cryptography kütüphanesini kullanır.
/// Hash formatı: {iterasyon}.{salt-base64}.{hash-base64}
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 210_000; // OWASP 2024+ önerisi
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string hash)
    {
        var parts = hash.Split('.', 3);
        if (parts.Length != 3) return false;

        if (!int.TryParse(parts[0], out var iterations)) return false;

        var salt = Convert.FromBase64String(parts[1]);
        var expectedHash = Convert.FromBase64String(parts[2]);

        var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, expectedHash.Length);

        // Zamanlama saldırılarına (timing attack) karşı sabit zamanlı karşılaştırma.
        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}
