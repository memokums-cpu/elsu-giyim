using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

/// <summary>
/// Bir ürünün tek bir beden+renk kombinasyonunu temsil eder.
/// Örnek: aynı tişörtün "Siyah / S" ve "Beyaz / M" varyantları ayrı
/// SKU ve stok kaydına sahiptir.
/// </summary>
public class ProductVariant : BaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty; // "S", "M", "42" vb.
    public string Color { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#000000";
    public int Stock { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
}
