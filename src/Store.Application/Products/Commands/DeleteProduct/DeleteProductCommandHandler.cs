using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions;

namespace Store.Application.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Guid>
{
    private readonly IUnitOfWork _uow;

    public DeleteProductCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _uow.GetRepository<Product>().GetAsync(w => w.Hash == request.Hash);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Hash);
        }

        _uow.GetRepository<Product>().Delete(product);

        await _uow.SaveChangesAsync(cancellationToken);

        return product.Hash;
    }
}