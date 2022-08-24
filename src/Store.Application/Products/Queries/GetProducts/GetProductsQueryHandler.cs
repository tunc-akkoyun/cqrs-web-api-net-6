using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Store.Application.Abstractions;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions;

namespace Store.Application.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IPagedList<GetProductsResponse>>
{
    private readonly IUnitOfWork _uow;

    public GetProductsQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<IPagedList<GetProductsResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _uow.GetRepository<Product>()
            .GetAllPagedAsync(w => (string.IsNullOrEmpty(request.name) || w.Name.Contains(request.name))
                            && (request.includeDeleted || w.DeletedUTC == null),
                 request.pageIndex,
                 request.pageSize);

        if (products == null || products.Data == null || products.Count == 0)
        {
            throw new ProductsNotFoundException();
        }

        return new PagedList<GetProductsResponse>(products.Data.Adapt<List<GetProductsResponse>>(), products.Count, request.pageIndex, request.pageSize);
    }
}