using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace ElsuGiyim.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Bir kategorideki yayınlanmış ürünleri listeler.</summary>
    /// <remarks>
    /// Beden/renk/fiyat gibi filtreler şu an frontend'de istemci
    /// tarafında uygulanıyor. İleride bu endpoint query parametreleri
    /// (örn. ?minPrice=&amp;size=) ile sunucu tarafı filtrelemeyi destekleyecek
    /// şekilde genişletilecek.
    /// </remarks>
    [HttpGet("category/{categorySlug}")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductListItemDto>>> GetByCategory(
        string categorySlug, CancellationToken cancellationToken)
    {
        var products = await _productService.GetByCategoryAsync(categorySlug, cancellationToken);
        return Ok(products);
    }

    /// <summary>Ana sayfadaki "Yeni Gelenler" rayı için ürünler.</summary>
    [HttpGet("new-arrivals")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductListItemDto>>> GetNewArrivals(
        CancellationToken cancellationToken)
    {
        var products = await _productService.GetNewArrivalsAsync(cancellationToken);
        return Ok(products);
    }

    /// <summary>Ana sayfadaki "Çok Satanlar" rayı için ürünler.</summary>
    [HttpGet("best-sellers")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductListItemDto>>> GetBestSellers(
        CancellationToken cancellationToken)
    {
        var products = await _productService.GetBestSellersAsync(cancellationToken);
        return Ok(products);
    }

    /// <summary>Tek bir ürünün detayını slug'a göre getirir.</summary>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailDto>> GetBySlug(
        string slug, CancellationToken cancellationToken)
    {
        var product = await _productService.GetBySlugAsync(slug, cancellationToken);

        if (product is null)
        {
            return NotFound(new { message = $"'{slug}' slug'ına sahip ürün bulunamadı." });
        }

        return Ok(product);
    }
}
