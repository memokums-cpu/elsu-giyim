using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElsuGiyim.Application.Common.Interfaces;

/// <summary>
/// Application katmanı EF Core'un kendisine değil, bu arayüze bağımlı.
/// Gerçek implementasyon (ApplicationDbContext) Infrastructure katmanında.
/// Bu sayede iş mantığı veritabanı teknolojisinden bağımsız kalıyor.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<ProductImage> ProductImages { get; }
    DbSet<ProductRelation> ProductRelations { get; }
    DbSet<InventoryMovement> InventoryMovements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
