using System;
using Store.Application.Abstractions.Messaging;

namespace Store.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Hash) : ICommand<Guid>;