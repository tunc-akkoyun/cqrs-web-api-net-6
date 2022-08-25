using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Products;

public sealed class ProductWithSameNameException : BadRequestException
{
    public ProductWithSameNameException(string name)
        : base($"The product with '{name}' name already exists.")
    {
    }
}

