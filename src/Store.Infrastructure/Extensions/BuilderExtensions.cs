using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Abstractions;
using Store.Domain.Primitives;

namespace Store.Infrastructure.Extensions;

public static class BuilderExtensions
{
    public static void HasExtended<T>(this EntityTypeBuilder<T> entity) where T : Entity, IEntityHash
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnOrder(-2);

        entity.HasIndex(e => e.Hash);
        entity.Property(e => e.Hash).IsRequired().HasDefaultValueSql("lower(newid())").HasColumnOrder(-1);
    }

    public static void HasAuditExtended<T>(this EntityTypeBuilder<T> entity) where T : Entity, IEntityHash, IEntityDateExtended
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnOrder(-5);

        entity.HasIndex(e => e.Hash);
        entity.Property(e => e.Hash).IsRequired().HasDefaultValueSql("lower(newid())").HasColumnOrder(-4);

        entity.Property(e => e.CreatedUTC).IsRequired().HasDefaultValue(DateTime.UtcNow).HasColumnOrder(-3);
        entity.Property(e => e.ModifiedUTC).HasColumnOrder(-2);
        entity.Property(e => e.DeletedUTC).HasColumnOrder(-1);
    }
}