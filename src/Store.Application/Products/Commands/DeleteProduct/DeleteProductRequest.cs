using System;

namespace Store.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductRequest(Guid Hash);