using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;

namespace Store.Application.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreateProductCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name);

        _uow.GetRepository<Product>().Add(product);

        await _uow.SaveChangesAsync(cancellationToken);

        return product.Hash;
    }
}