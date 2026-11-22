using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElsuGiyim.Infrastructure.Persistence.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.Property(v => v.Sku).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Size).IsRequired().HasMaxLength(20);
        builder.Property(v => v.Color).IsRequired().HasMaxLength(50);
        builder.Property(v => v.ColorHex).IsRequired().HasMaxLength(7);

        // Her SKU, tüm sistemde benzersiz olmalı.
        builder.HasIndex(v => v.Sku).IsUnique();
    }
}
