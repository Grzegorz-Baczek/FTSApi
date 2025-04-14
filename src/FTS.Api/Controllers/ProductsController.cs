using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Products.Handlers;

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
}