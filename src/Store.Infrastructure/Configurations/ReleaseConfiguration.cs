using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;
using Store.Infrastructure.Extensions;

namespace Store.Infrastructure.Configurations;

internal sealed class ReleaseConfiguration : IEntityTypeConfiguration<Release>
{
    public void Configure(EntityTypeBuilder<Release> builder)
    {
        builder.ToTable("Releases");

        builder.HasExtended();

        builder.Property(p => p.Version).IsRequired().HasMaxLength(10);
        builder.Property(p => p.Notes).IsRequired().HasMaxLength(500);
        builder.Property(p => p.ReleaseUTC).IsRequired();
    }
}