using ElsuGiyim.Application.DTOs;

namespace ElsuGiyim.Application.Categories;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
