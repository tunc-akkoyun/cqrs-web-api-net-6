using System;
using Store.Application.Abstractions.Messaging;

namespace Store.Application.Products.Queries.GetProductByHash;

public sealed record GetProductByHashQuery(Guid ProductHash) : IQuery<GetProductByHashResponse>;