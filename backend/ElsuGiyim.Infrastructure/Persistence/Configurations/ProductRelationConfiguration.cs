using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElsuGiyim.Infrastructure.Persistence.Configurations;

public class ProductRelationConfiguration : IEntityTypeConfiguration<ProductRelation>
{
    public void Configure(EntityTypeBuilder<ProductRelation> builder)
    {
        builder.HasOne(r => r.Product)
            .WithMany()
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // RelatedProduct, Product ile aynı tabloya işaret ettiği için EF Core'un
        // cascade delete çakışması yaşamaması adına Restrict kullanıyoruz.
        builder.HasOne(r => r.RelatedProduct)
            .WithMany()
            .HasForeignKey(r => r.RelatedProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.ProductId, r.RelatedProductId, r.Type }).IsUnique();
    }
}
