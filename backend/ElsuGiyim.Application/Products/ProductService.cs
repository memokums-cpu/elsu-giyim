using System.Text.Json;
using ElsuGiyim.Application.Common.Interfaces;
using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Application.Products.Admin;
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

    // --- Yönetim paneli ---

    public async Task<IReadOnlyList<ProductListItemDto>> GetAllForAdminAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return products.Select(MapToListItem).ToList();
    }

    public async Task<ProductDetailDto> CreateAsync(
        CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Name = request.Name,
            Slug = request.Slug,
            CategoryId = request.CategoryId,
            SubCategory = request.SubCategory,
            Price = request.Price,
            SalePrice = request.SalePrice,
            Description = request.Description,
            Fabric = request.Fabric,
            CareInstructions = request.CareInstructions,
            Fit = ParseFit(request.Fit),
            ModelInfo = request.ModelInfo,
            SizeChartJson = SerializeSizeChart(request.SizeChart),
            IsNew = request.IsNew,
            IsBestSeller = request.IsBestSeller,
            IsPublished = true,
            Variants = request.Variants.Select(v => new ProductVariant
            {
                Sku = v.Sku,
                Size = v.Size,
                Color = v.Color,
                ColorHex = v.ColorHex,
                Stock = v.Stock,
            }).ToList(),
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return await GetBySlugForAdminAsync(product.Id, cancellationToken)
            ?? throw new InvalidOperationException("Ürün oluşturuldu ama tekrar okunamadı.");
    }

    public async Task<ProductDetailDto?> UpdateAsync(
        Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product is null) return null;

        product.Name = request.Name;
        product.Slug = request.Slug;
        product.CategoryId = request.CategoryId;
        product.SubCategory = request.SubCategory;
        product.Price = request.Price;
        product.SalePrice = request.SalePrice;
        product.Description = request.Description;
        product.Fabric = request.Fabric;
        product.CareInstructions = request.CareInstructions;
        product.Fit = ParseFit(request.Fit);
        product.ModelInfo = request.ModelInfo;
        product.SizeChartJson = SerializeSizeChart(request.SizeChart);
        product.IsNew = request.IsNew;
        product.IsBestSeller = request.IsBestSeller;
        product.IsPublished = request.IsPublished;
        product.UpdatedAtUtc = DateTime.UtcNow;

        // Varyantları SKU'ya göre eşleştirerek senkronize et: listede
        // olmayanları sil, ortak olanları güncelle, yeni olanları ekle.
        var incomingBySku = request.Variants.ToDictionary(v => v.Sku);
        var existingBySku = product.Variants.ToDictionary(v => v.Sku);

        foreach (var existing in product.Variants.ToList())
        {
            if (!incomingBySku.ContainsKey(existing.Sku))
            {
                product.Variants.Remove(existing);
            }
        }

        foreach (var incoming in request.Variants)
        {
            if (existingBySku.TryGetValue(incoming.Sku, out var existingVariant))
            {
                existingVariant.Size = incoming.Size;
                existingVariant.Color = incoming.Color;
                existingVariant.ColorHex = incoming.ColorHex;
                existingVariant.Stock = incoming.Stock;
                existingVariant.UpdatedAtUtc = DateTime.UtcNow;
            }
            else
            {
                product.Variants.Add(new ProductVariant
                {
                    Sku = incoming.Sku,
                    Size = incoming.Size,
                    Color = incoming.Color,
                    ColorHex = incoming.ColorHex,
                    Stock = incoming.Stock,
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return await GetBySlugForAdminAsync(product.Id, cancellationToken);
    }

    public async Task<bool> SetPublishedAsync(
        Guid id, bool isPublished, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null) return false;

        product.IsPublished = isPublished;
        product.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> AdjustStockAsync(
        Guid variantId, AdjustStockRequest request, CancellationToken cancellationToken = default)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == variantId, cancellationToken);

        if (variant is null) return false;

        var difference = request.NewStock - variant.Stock;
        variant.Stock = request.NewStock;
        variant.UpdatedAtUtc = DateTime.UtcNow;

        _context.InventoryMovements.Add(new InventoryMovement
        {
            ProductVariantId = variant.Id,
            Type = difference >= 0 ? InventoryMovementType.StockIn : InventoryMovementType.StockOut,
            Quantity = Math.Abs(difference),
            Note = request.Note,
        });

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// GetBySlugAsync'in yayın durumuna bakmaksızın (admin için) ID ile
    /// çalışan hali — oluşturma/güncelleme sonrası tam detay döndürmek için.
    /// </summary>
    private async Task<ProductDetailDto?> GetBySlugForAdminAsync(
        Guid productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

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

    private static FitType? ParseFit(string? fit) =>
        string.IsNullOrWhiteSpace(fit) ? null : Enum.Parse<FitType>(fit);

    private static string? SerializeSizeChart(IReadOnlyList<SizeChartRowDto> sizeChart) =>
        sizeChart.Count == 0 ? null : JsonSerializer.Serialize(sizeChart);

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
