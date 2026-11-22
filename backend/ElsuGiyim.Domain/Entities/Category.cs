using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    /// <summary>URL'de kullanılan kısa ad, örn. "kadin", "erkek".</summary>
    public string Slug { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    // Alt kategori desteği (örn. "Kadın" > "Elbise")
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
