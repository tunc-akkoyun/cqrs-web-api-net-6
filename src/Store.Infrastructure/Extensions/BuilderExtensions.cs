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
        entity.Property(e => e.Id).HasColumnOrder(-5);
        entity.Property(e => e.Hash).IsRequired().HasDefaultValueSql("lower(newid())").HasColumnOrder(-4);
    }

    public static void HasFullExtended<T>(this EntityTypeBuilder<T> entity) where T : Entity, IEntityHash, IEntityDateExtended
    {
        HasExtended(entity);

        entity.Property(e => e.CreatedUTC).IsRequired().HasDefaultValue(DateTime.UtcNow).HasColumnOrder(-3);
        entity.Property(e => e.ModifiedUTC).HasColumnOrder(-2);
        entity.Property(e => e.DeletedUTC).HasColumnOrder(-1);
    }
}