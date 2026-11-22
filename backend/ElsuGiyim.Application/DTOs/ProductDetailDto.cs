namespace ElsuGiyim.Application.DTOs;

public record ProductDetailDto(
    string Slug,
    string Name,
    string Category,
    string SubCategory,
    decimal Price,
    decimal? SalePrice,
    string Description,
    string Fabric,
    string CareInstructions,
    string? Fit,
    string? ModelInfo,
    IReadOnlyList<ProductVariantDto> Variants,
    IReadOnlyList<string> ImageUrls,
    IReadOnlyList<SizeChartRowDto> SizeChart,
    IReadOnlyList<ProductListItemDto> OutfitProducts,
    IReadOnlyList<ProductListItemDto> SimilarProducts
);
