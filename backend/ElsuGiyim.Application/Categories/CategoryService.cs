using ElsuGiyim.Application.Common.Interfaces;
using ElsuGiyim.Application.DTOs;
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
            .Select(c => new CategoryDto(c.Id, c.Name, c.Slug, c.DisplayOrder))
            .ToListAsync(cancellationToken);
    }
}
