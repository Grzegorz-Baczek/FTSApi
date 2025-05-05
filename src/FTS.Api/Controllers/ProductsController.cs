using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Products.Handlers;
using FTS.Core.Entities;
using FTS.Application.Queries.Products.Handlers;

namespace FTS.Api.Controllers;

[Route("product")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateProduct(CreateProductCommand command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [HttpGet("/products")]
    public async Task<IEnumerable<Product>> GetProducts([FromQuery] GetProductsQuery query,
        CancellationToken ct)
    {
        var products = await mediator.Send(query, ct);
        return products;
    }
}