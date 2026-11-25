using System.Text.Json;
using ElsuGiyim.Application.Common.Interfaces;
using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElsuGiyim.Application.Products;

public class ProductService : IProductService
{
    private readonly IApplicationDbContext _context;

    public ProductService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductListItemDto>> GetByCategoryAsync(
        string categorySlug, CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Where(p => p.IsPublished && p.Category.Slug == categorySlug)
            .ToListAsync(cancellationToken);

        return products.Select(MapToListItem).ToList();
    }

    public async Task<IReadOnlyList<ProductListItemDto>> GetNewArrivalsAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Where(p => p.IsPublished && p.IsNew)
            .ToListAsync(cancellationToken);

        return products.Select(MapToListItem).ToList();
    }

    public async Task<IReadOnlyList<ProductListItemDto>> GetBestSellersAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Where(p => p.IsPublished && p.IsBestSeller)
            .ToListAsync(cancellationToken);

        return products.Select(MapToListItem).ToList();
    }

    public async Task<ProductDetailDto?> GetBySlugAsync(
        string slug, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.IsPublished && p.Slug == slug, cancellationToken);

        if (product is null) return null;

        var relations = await _context.ProductRelations
            .Include(r => r.RelatedProduct).ThenInclude(p => p.Category)
            .Include(r => r.RelatedProduct).ThenInclude(p => p.Variants)
            .Where(r => r.ProductId == product.Id)
            .ToListAsync(cancellationToken);

        var outfitProducts = relations
            .Where(r => r.Type == ProductRelationType.Outfit)
            .Select(r => MapToListItem(r.RelatedProduct))
            .ToList();

        var similarProducts = relations
            .Where(r => r.Type == ProductRelationType.Similar)
            .Select(r => MapToListItem(r.RelatedProduct))
            .ToList();

        return new ProductDetailDto(
            product.Slug,
            product.Name,
            product.Category.Slug,
            product.SubCategory,
            product.Price,
            product.SalePrice,
            product.Description,
            product.Fabric,
            product.CareInstructions,
            product.Fit?.ToString(),
            product.ModelInfo,
            product.Variants.Select(MapVariant).ToList(),
            product.Images.OrderBy(i => i.DisplayOrder).Select(i => i.Url).ToList(),
            ParseSizeChart(product.SizeChartJson),
            outfitProducts,
            similarProducts
        );
    }

    private static IReadOnlyList<SizeChartRowDto> ParseSizeChart(string? sizeChartJson)
    {
        if (string.IsNullOrWhiteSpace(sizeChartJson)) return Array.Empty<SizeChartRowDto>();

        try
        {
            return (IReadOnlyList<SizeChartRowDto>?)JsonSerializer.Deserialize<List<SizeChartRowDto>>(sizeChartJson)
                ?? Array.Empty<SizeChartRowDto>();
        }
        catch (JsonException)
        {
            // Bozuk veri olması durumunda API'nin çökmesindense boş liste dönmesi tercih edilir.
            return Array.Empty<SizeChartRowDto>();
        }
    }

    private static ProductListItemDto MapToListItem(Product product) => new(
        product.Slug,
        product.Name,
        product.Category.Slug,
        product.SubCategory,
        product.Price,
        product.SalePrice,
        product.Fit?.ToString(),
        product.IsNew,
        product.IsBestSeller,
        product.Variants.Select(MapVariant).ToList()
    );

    private static ProductVariantDto MapVariant(ProductVariant variant) => new(
        variant.Sku,
        variant.Size,
        variant.Color,
        variant.ColorHex,
        variant.Stock
    );
}
