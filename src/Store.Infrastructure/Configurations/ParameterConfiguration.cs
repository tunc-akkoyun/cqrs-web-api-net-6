using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrastructure.Configurations;

internal sealed class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.ToTable("Parameters");

        builder.HasKey(p => p.Key);

        builder.Property(p => p.Value).IsRequired().HasMaxLength(500);
    }
}