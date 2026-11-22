namespace ElsuGiyim.Application.DTOs;

/// <summary>
/// Ürün listeleme (kategori sayfası, ana sayfa rayları) için hafif model.
/// Tam açıklama, kumaş bilgisi gibi alanları içermez — onlar detay
/// sayfasında (ProductDetailDto) gelir.
/// </summary>
public record ProductListItemDto(
    string Slug,
    string Name,
    string Category,
    string SubCategory,
    decimal Price,
    decimal? SalePrice,
    string? Fit,
    bool IsNew,
    bool IsBestSeller,
    IReadOnlyList<ProductVariantDto> Variants
);
