using ElsuGiyim.Application.DTOs;

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
}
