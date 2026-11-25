using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

/// <summary>
/// Sabit rol adları. Veritabanında ayrıca Role tablosu olarak da tutulur
/// (yönetim panelinden görüntülenebilmesi için), ama kod içinde yetki
/// kontrolü yaparken bu sabitler kullanılır.
/// </summary>
public static class RoleNames
{
    public const string Admin = "Admin";
    public const string UrunYoneticisi = "UrunYoneticisi";
    public const string SiparisYoneticisi = "SiparisYoneticisi";
}

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
