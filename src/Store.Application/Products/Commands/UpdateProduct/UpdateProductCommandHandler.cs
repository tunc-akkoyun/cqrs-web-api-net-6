using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions;

namespace Store.Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public UpdateProductCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _uow.GetRepository<Product>().GetAsync(w => w.Hash == request.Hash);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Hash);
        }

        product.Name = request.Name;

        _uow.GetRepository<Product>().Update(product);

        var result = await _uow.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}