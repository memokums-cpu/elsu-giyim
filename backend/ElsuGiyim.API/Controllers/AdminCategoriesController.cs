using ElsuGiyim.Application.Categories;
using ElsuGiyim.Application.Categories.Admin;
using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElsuGiyim.API.Controllers;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = $"{RoleNames.Admin},{RoleNames.UrunYoneticisi}")]
public class AdminCategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public AdminCategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>Pasif olanlar dahil tüm kategorileri listeler.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.GetAllForAdminAsync(cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CategoryDto>> Create(
        [FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { }, category);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> Update(
        Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.UpdateAsync(id, request, cancellationToken);
        if (category is null)
        {
            return NotFound(new { message = "Kategori bulunamadı." });
        }
        return Ok(category);
    }

    /// <summary>Kategoriyi pasif hale getirir (kalıcı silme yapılmaz).</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var success = await _categoryService.DeactivateAsync(id, cancellationToken);
        if (!success)
        {
            return NotFound(new { message = "Kategori bulunamadı." });
        }
        return NoContent();
    }
}
