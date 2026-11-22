using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

public enum InventoryMovementType
{
    StockIn,   // Giriş (yeni stok, iade)
    StockOut,  // Çıkış (satış, zayiat)
    Adjustment, // Manuel düzeltme
}

/// <summary>
/// Her stok değişikliğini kaydeder, böylece "bu varyantın stoğu neden
/// değişti" sorusu her zaman yanıtlanabilir (audit amaçlı).
/// </summary>
public class InventoryMovement : BaseEntity
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;

    public InventoryMovementType Type { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}
