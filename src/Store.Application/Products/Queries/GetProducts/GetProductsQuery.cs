using Store.Application.Abstractions;
using Store.Application.Abstractions.Messaging;

namespace Store.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery(string name, bool includeDeleted, int pageIndex = 0, int pageSize = int.MaxValue) : IQuery<IPagedList<GetProductsResponse>>;