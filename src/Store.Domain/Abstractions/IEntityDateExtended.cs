using System;

namespace Store.Domain.Abstractions;

public interface IEntityDateExtended
{
    DateTime CreatedUTC { get; set; }
    DateTime? ModifiedUTC { get; set; }
    DateTime? DeletedUTC { get; set; }
}