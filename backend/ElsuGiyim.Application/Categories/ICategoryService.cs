using ElsuGiyim.Application.Categories.Admin;
using ElsuGiyim.Application.DTOs;

namespace ElsuGiyim.Application.Categories;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Yönetim paneli: aktif/pasif tüm kategorileri listeler.</summary>
    Task<IReadOnlyList<CategoryDto>> GetAllForAdminAsync(CancellationToken cancellationToken = default);

    Task<CategoryDto> CreateAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>Kategori bulunamazsa null döner.</summary>
    Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Kategoriyi pasif hale getirir (hard delete yapmaz — ürünler bu
    /// kategoriye bağlı olabilir, veri bütünlüğünü bozmamak için).
    /// Kategori bulunamazsa false döner.
    /// </summary>
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}
