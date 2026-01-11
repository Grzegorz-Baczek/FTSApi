using FTS.Application.Commands.Products.Handlers;
using FTS.Application.Queries.Products.Handlers;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Products.Handlers.DeleteProduct;

namespace FTS.Api.Controllers;

[Route("api")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost("product")]
    public async Task<ActionResult> CreateProduct(CreateProductCommand command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [HttpGet("products")]
    public async Task<IEnumerable<Product>> GetProducts([FromQuery] GetProductsQuery query,
        CancellationToken ct)
    {
        var products = await mediator.Send(query, ct);
        return products;
    }

    [HttpGet("product/{id:guid}")]
    public async Task<Product> GetProduct(Guid id, CancellationToken ct)
    {
        var product = await mediator.Send(new GetProductQuery(id), ct);
        return product;
    }

    [HttpDelete("product/{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id,
        CancellationToken ct)
    {
        await mediator.Send(new DeleteProductCommand(id), ct);
        return NoContent();
    }
}