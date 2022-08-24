using System;
using Store.Domain.Abstractions;
using Store.Domain.Primitives;

namespace Store.Domain.Entities;

public sealed class Product : EntityHash, IEntityDateExtended
{
    public Product(string name) => Name = name;

    public string Name { get; set; }
    public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedUTC { get; set; }
    public DateTime? DeletedUTC { get; set; }
}