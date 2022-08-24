using System;
using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productHash)
        : base($"The product with the identifier {productHash} was not found.")
    {
    }
}

public sealed class ProductsNotFoundException : NotFoundException
{
    public ProductsNotFoundException()
        : base($"Products was not found.")
    {
    }
}