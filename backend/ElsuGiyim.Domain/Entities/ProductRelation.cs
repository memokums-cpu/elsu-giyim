using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

public enum ProductRelationType
{
    Outfit,   // "Ürünü Tamamla" - kombin önerisi
    Similar,  // "Benzer Ürünler"
}

/// <summary>
/// Bir ürünün başka bir ürünle ilişkisini tutar (kombin önerisi veya
/// benzer ürün). Yönetim panelinden (Aşama 4) bu ilişkiler
/// oluşturulabilecek.
/// </summary>
public class ProductRelation : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid RelatedProductId { get; set; }
    public Product RelatedProduct { get; set; } = null!;

    public ProductRelationType Type { get; set; }
}
