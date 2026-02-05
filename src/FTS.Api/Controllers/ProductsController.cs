using FTS.Application.Queries.Products.Handlers;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Products.Handlers.DeleteProduct;
using Microsoft.AspNetCore.Authorization;
using FTS.Core.Security;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet("product/{id:guid}")]
    public async Task<Product> GetProduct(Guid id, CancellationToken ct)
    {
        var product = await mediator.Send(new GetProductQuery(id), ct);
        return product;
    }

    [Authorize(Roles = Roles.User)]
    [HttpDelete("product/{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id,
        CancellationToken ct)
    {
        await mediator.Send(new DeleteProductCommand(id), ct);
        return NoContent();
    }
}