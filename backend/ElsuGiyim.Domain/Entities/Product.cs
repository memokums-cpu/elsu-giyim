using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    /// <summary>URL'de kullanılan kısa ad, örn. "keten-gomlek".</summary>
    public string Slug { get; set; } = string.Empty;

    public string SubCategory { get; set; } = string.Empty; // "Tişört", "Pantolon" vb.

    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Fabric { get; set; } = string.Empty;
    public string CareInstructions { get; set; } = string.Empty;
    public FitType? Fit { get; set; }
    public string? ModelInfo { get; set; }

    /// <summary>
    /// Beden tablosu, JSON dizi olarak saklanıyor: [{"size":"S","chest":"96-100 cm"}, ...].
    /// İleride ayrı bir SizeChartRow tablosuna taşınabilir; şimdilik basit tutuyoruz.
    /// </summary>
    public string? SizeChartJson { get; set; }

    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsPublished { get; set; } = true;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}
