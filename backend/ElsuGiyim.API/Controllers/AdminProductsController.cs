using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Application.Products;
using ElsuGiyim.Application.Products.Admin;
using ElsuGiyim.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElsuGiyim.API.Controllers;

[ApiController]
[Route("api/admin/products")]
[Authorize(Roles = $"{RoleNames.Admin},{RoleNames.UrunYoneticisi}")]
public class AdminProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public AdminProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Yayından kaldırılmış olanlar dahil tüm ürünleri listeler.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductListItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _productService.GetAllForAdminAsync(cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductDetailDto>> Create(
        [FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { }, product);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailDto>> Update(
        Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.UpdateAsync(id, request, cancellationToken);
        if (product is null)
        {
            return NotFound(new { message = "Ürün bulunamadı." });
        }
        return Ok(product);
    }

    /// <summary>Ürünü yayından kaldırır (müşteri tarafında artık görünmez).</summary>
    [HttpPost("{id:guid}/unpublish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unpublish(Guid id, CancellationToken cancellationToken)
    {
        var success = await _productService.SetPublishedAsync(id, false, cancellationToken);
        if (!success) return NotFound(new { message = "Ürün bulunamadı." });
        return NoContent();
    }

    /// <summary>Daha önce yayından kaldırılmış bir ürünü tekrar yayına alır.</summary>
    [HttpPost("{id:guid}/publish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken)
    {
        var success = await _productService.SetPublishedAsync(id, true, cancellationToken);
        if (!success) return NotFound(new { message = "Ürün bulunamadı." });
        return NoContent();
    }

    /// <summary>Bir varyantın stoğunu günceller (stok hareketi olarak kaydedilir).</summary>
    [HttpPatch("variants/{variantId:guid}/stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdjustStock(
        Guid variantId, [FromBody] AdjustStockRequest request, CancellationToken cancellationToken)
    {
        var success = await _productService.AdjustStockAsync(variantId, request, cancellationToken);
        if (!success) return NotFound(new { message = "Varyant bulunamadı." });
        return NoContent();
    }
}
