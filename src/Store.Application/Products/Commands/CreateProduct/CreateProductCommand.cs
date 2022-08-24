using System;
using Store.Application.Abstractions.Messaging;

namespace Store.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name) : ICommand<Guid>;