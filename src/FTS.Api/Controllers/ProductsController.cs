using FTS.Application.Commands.Products.Handlers.DeleteProduct;
using FTS.Application.Queries.Products.Handlers;
using FTS.Core.Entities;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = Roles.Admin)]

public class ProductsController(IMediator mediator) : ControllerBase
{
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