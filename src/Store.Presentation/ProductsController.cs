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

namespace Store.Presentation;

/// <summary>
/// Represents the products controller.
/// </summary>
[Route("api/v1/products")]
public sealed class ProductsController : ApiController
{
    /// <summary>
    /// Gets the product with the specified identifier, if it exists.
    /// </summary>
    /// <param name="hash">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product with the specified identifier, if it exists.</returns>
    [HttpGet("{hash}")]
    [ProducesResponseType(typeof(GetProductByHashResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(Guid hash, CancellationToken cancellationToken)
        => Ok(await Sender.Send(new GetProductByHashQuery(hash), cancellationToken));

    /// <summary>
    /// Gets all the product with pagination, if it exists.
    /// </summary>
    /// <param name="name">Product name</param>
    /// <param name="includeDeleted">Include deleted</param>
    /// <param name="pageIndex">The page index.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product with the specified identifier, if it exists.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IPagedList<GetProductsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts(string name, int pageIndex, int pageSize, bool includeDeleted = false, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetProductsQuery(name, includeDeleted, pageIndex, pageSize), cancellationToken));

    /// <summary>
    /// Creates a new product based on the specified request.
    /// </summary>
    /// <param name="request">The create product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created product.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        await Sender.Send(request.Adapt<CreateProductCommand>(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Updates a product based on the specified request.
    /// </summary>
    /// <param name="request">The update product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the updated product.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await Sender.Send(request.Adapt<UpdateProductCommand>(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes a product based on the specified request.
    /// </summary>
    /// <param name="request">The delete product request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the deleted product.</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(
        [FromBody] DeleteProductRequest request,
        CancellationToken cancellationToken)
    {
        await Sender.Send(request.Adapt<DeleteProductCommand>(), cancellationToken);

        return NoContent();
    }
}