namespace ElsuGiyim.Domain.Entities;

/// <summary>
/// User ve Role arasındaki çoka-çok ilişki. Ayrı bir BaseEntity'den
/// türetmiyoruz çünkü bu saf bir ilişki tablosu — kendi oluşturulma
/// tarihi gibi bilgilere ihtiyacı yok.
/// </summary>
public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
