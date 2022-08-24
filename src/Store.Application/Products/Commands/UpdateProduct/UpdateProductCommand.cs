using System;
using Store.Application.Abstractions.Messaging;

namespace Store.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(Guid Hash, string Name) : ICommand<bool>;