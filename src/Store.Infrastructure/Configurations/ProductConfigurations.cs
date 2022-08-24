using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;
using Store.Infrastructure.Extensions;

namespace Store.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasFullExtended();

        builder.Property(p => p.Name).HasMaxLength(100);
    }
}