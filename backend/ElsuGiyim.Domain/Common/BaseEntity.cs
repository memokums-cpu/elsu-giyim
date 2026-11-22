namespace ElsuGiyim.Domain.Common;

/// <summary>
/// Tüm entity'lerin ortak temel sınıfı. Id olarak Guid kullanıyoruz,
/// böylece ID'ler veritabanına yazılmadan önce de (örn. ilişkili kayıtları
/// aynı anda oluştururken) üretilebiliyor.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
