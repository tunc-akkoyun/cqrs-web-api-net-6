using System;

namespace Store.Domain.Abstractions;

public interface IEntityHash
{
    Guid Hash { get; set; }
}