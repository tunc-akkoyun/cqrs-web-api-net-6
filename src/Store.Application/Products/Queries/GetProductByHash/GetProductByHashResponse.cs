using System;

namespace Store.Application.Products.Queries.GetProductByHash;

public sealed record GetProductByHashResponse(Guid Hash, string Name);