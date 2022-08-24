using System;

namespace Store.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductRequest(Guid Hash, string Name);