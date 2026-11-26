using ElsuGiyim.Application.Categories.Admin;
using ElsuGiyim.Application.Common.Interfaces;
using ElsuGiyim.Application.DTOs;
using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElsuGiyim.Application.Categories;

public class CategoryService : ICategoryService
{
    private readonly IApplicationDbContext _context;

    public CategoryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .Select(c => MapToDto(c))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllForAdminAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .OrderBy(c => c.DisplayOrder)
            .Select(c => MapToDto(c))
            .ToListAsync(cancellationToken);
    }

    public async Task<CategoryDto> CreateAsync(
        CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = new Category
        {
            Name = request.Name,
            Slug = request.Slug,
            DisplayOrder = request.DisplayOrder,
            ParentCategoryId = request.ParentCategoryId,
            IsActive = true,
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(category);
    }

    public async Task<CategoryDto?> UpdateAsync(
        Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category is null) return null;

        category.Name = request.Name;
        category.Slug = request.Slug;
        category.DisplayOrder = request.DisplayOrder;
        category.IsActive = request.IsActive;
        category.ParentCategoryId = request.ParentCategoryId;
        category.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(category);
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category is null) return false;

        category.IsActive = false;
        category.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static CategoryDto MapToDto(Category category) => new(
        category.Id,
        category.Name,
        category.Slug,
        category.DisplayOrder,
        category.IsActive,
        category.ParentCategoryId
    );
}
