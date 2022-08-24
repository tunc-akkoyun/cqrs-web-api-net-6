using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Abstractions;
using Store.Application.Products.Commands.CreateProduct;
using Store.Application.Products.Commands.DeleteProduct;
using Store.Application.Products.Commands.UpdateProduct;
using Store.Application.Products.Queries.GetProductByHash;
using Store.Application.Products.Queries.GetProducts;
using Swashbuckle.AspNetCore.Annotations;

namespace Store.Presentation;

/// <summary>
/// Represents the products controller.
/// </summary>
[Route("api/v1/products")]
public sealed class ProductsController : ApiController
{
    [HttpGet("{hash}")]
    [SwaggerResponse(200, Type = typeof(GetProductByHashResponse))]
    [SwaggerOperation(Summary = "Get", Description = "Gets the product with the specified identifier, if it exists.", OperationId = "getProduct")]
    public async Task<IActionResult> GetProduct(Guid hash, CancellationToken cancellationToken)
        => Ok(await Sender.Send(new GetProductByHashQuery(hash), cancellationToken));

    [HttpGet]
    [SwaggerResponse(200, Type = typeof(IPagedList<GetProductsResponse>))]
    [SwaggerOperation(Summary = "List", Description = "Gets all the product with pagination, if it exists.", OperationId = "getProducts")]
    public async Task<IActionResult> GetProducts(string name, int pageIndex, int pageSize, bool includeDeleted = false, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetProductsQuery(name, includeDeleted, pageIndex, pageSize), cancellationToken));

    [HttpPost]
    [SwaggerResponse(201)]
    [SwaggerOperation(Summary = "Create", Description = "Creates a new product based on the specified request.", OperationId = "createProduct")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.Adapt<CreateProductCommand>(), cancellationToken);

        return CreatedAtAction(nameof(GetProduct), result);
    }

    [HttpPut]
    [SwaggerResponse(204)]
    [SwaggerOperation(Summary = "Update", Description = "Updates a product based on the specified request.", OperationId = "updateProduct")]
    public async Task<IActionResult> UpdateProduct(
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await Sender.Send(request.Adapt<UpdateProductCommand>(), cancellationToken);

        return NoContent();
    }

    [HttpDelete]
    [SwaggerResponse(204)]
    [SwaggerOperation(Summary = "Delete", Description = "Deletes a product based on the specified request.", OperationId = "deleteProduct")]
    public async Task<IActionResult> DeleteProduct(
        [FromBody] DeleteProductRequest request,
        CancellationToken cancellationToken)
    {
        await Sender.Send(request.Adapt<DeleteProductCommand>(), cancellationToken);

        return NoContent();
    }
}