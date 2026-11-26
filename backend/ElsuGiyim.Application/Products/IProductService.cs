using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Application.Products.Admin;

namespace ElsuGiyim.Application.Products;

public interface IProductService
{
    Task<IReadOnlyList<ProductListItemDto>> GetByCategoryAsync(
        string categorySlug, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductListItemDto>> GetNewArrivalsAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductListItemDto>> GetBestSellersAsync(
        CancellationToken cancellationToken = default);

    Task<ProductDetailDto?> GetBySlugAsync(
        string slug, CancellationToken cancellationToken = default);

    // --- Yönetim paneli ---

    /// <summary>Yayından kaldırılmış olanlar dahil tüm ürünleri listeler.</summary>
    Task<IReadOnlyList<ProductListItemDto>> GetAllForAdminAsync(CancellationToken cancellationToken = default);

    Task<ProductDetailDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Ürün bulunamazsa null döner.</summary>
    Task<ProductDetailDto?> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Ürünü yayından kaldırır/yayına alır. Ürün bulunamazsa false döner.</summary>
    Task<bool> SetPublishedAsync(Guid id, bool isPublished, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bir varyantın stoğunu değiştirir ve bunu InventoryMovement olarak
    /// kaydeder. Varyant bulunamazsa false döner.
    /// </summary>
    Task<bool> AdjustStockAsync(Guid variantId, AdjustStockRequest request, CancellationToken cancellationToken = default);
}
