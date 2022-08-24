using System;
using Store.Domain.Abstractions;

namespace Store.Domain.Primitives;

public abstract class EntityHash : Entity, IEntityHash
{
    public Guid Hash { get; set; }
}