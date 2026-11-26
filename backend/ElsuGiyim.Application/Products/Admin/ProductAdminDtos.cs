using ElsuGiyim.Application.DTOs;

namespace ElsuGiyim.Application.Products.Admin;

public record CreateVariantRequest(
    string Sku,
    string Size,
    string Color,
    string ColorHex,
    int Stock
);

public record CreateProductRequest(
    string Name,
    string Slug,
    Guid CategoryId,
    string SubCategory,
    decimal Price,
    decimal? SalePrice,
    string Description,
    string Fabric,
    string CareInstructions,
    string? Fit,
    string? ModelInfo,
    IReadOnlyList<SizeChartRowDto> SizeChart,
    IReadOnlyList<CreateVariantRequest> Variants,
    bool IsNew,
    bool IsBestSeller
);

/// <summary>
/// Güncellemede varyantlar SKU'ya göre eşleştirilir: listede olmayan
/// mevcut SKU'lar silinir, yeni SKU'lar eklenir, ortak olanların stok/renk
/// bilgisi güncellenir. Stok tek başına değiştirmek için ayrı
/// /variants/{id}/stock endpoint'i kullanılmalı (stok hareketi geçmişi
/// orada tutuluyor).
/// </summary>
public record UpdateProductRequest(
    string Name,
    string Slug,
    Guid CategoryId,
    string SubCategory,
    decimal Price,
    decimal? SalePrice,
    string Description,
    string Fabric,
    string CareInstructions,
    string? Fit,
    string? ModelInfo,
    IReadOnlyList<SizeChartRowDto> SizeChart,
    IReadOnlyList<CreateVariantRequest> Variants,
    bool IsNew,
    bool IsBestSeller,
    bool IsPublished
);

public record AdjustStockRequest(int NewStock, string? Note);
