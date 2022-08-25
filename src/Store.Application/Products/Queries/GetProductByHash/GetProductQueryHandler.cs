using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions.Products;

namespace Store.Application.Products.Queries.GetProductByHash;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductByHashQuery, GetProductByHashResponse>
{
    private readonly IUnitOfWork _uow;

    public GetProductQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<GetProductByHashResponse> Handle(
        GetProductByHashQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _uow.GetRepository<Product>()
            .GetAsync(w => w.Hash == request.ProductHash && w.DeletedUTC == null);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductHash);
        }

        return new GetProductByHashResponse(product.Hash, product.Name);
    }
}