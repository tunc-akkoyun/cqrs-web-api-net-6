using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;
using Store.Infrastructure.Extensions;

namespace Store.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasAuditExtended();

        builder.HasIndex(p => p.Name);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.HasIndex(p => p.Email);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Password).IsRequired().HasMaxLength(250);
    }
}