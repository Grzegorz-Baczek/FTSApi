using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Application.Handlers.ShoppingLists.Commands.AddShoppingListItem;
using FTS.Application.Handlers.ShoppingLists.Commands.AddItemsFromImage;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api/shopping-lists")]
[Authorize]
public class ShoppingListController : ControllerBase
{
    private readonly IShoppingListRepository _repository;
    private readonly IOcrService _ocrService;
    private readonly IMediator _mediator;

    public ShoppingListController(IShoppingListRepository repository, IOcrService ocrService, IMediator mediator)
    {
        _repository = repository;
        _ocrService = ocrService;
        _mediator = mediator;
    }

    private Guid? TryGetUserId()
    {
        // Try "sub" (when MapInboundClaims=false) and ClaimTypes.NameIdentifier (when mapped)
        var claim = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                    ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Guid.TryParse(claim, out var userId))
            return userId;
        return null;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all shopping lists for current user")]
    [ProducesResponseType(typeof(IReadOnlyList<ShoppingListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ShoppingListDto>>> GetAll(CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var lists = await _repository.GetByUserIdAsync(userId.Value, ct);
        return Ok(lists.Select(MapToDto).ToList());
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get a shopping list by ID")]
    [ProducesResponseType(typeof(ShoppingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShoppingListDto>> Get(Guid id, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var list = await _repository.GetAsync(id, ct);
        if (list is null || list.UserId != userId.Value)
            return NotFound();

        return Ok(MapToDto(list));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new shopping list")]
    [ProducesResponseType(typeof(ShoppingListDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ShoppingListDto>> Create([FromBody] CreateShoppingListDto dto, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var list = ShoppingList.Create(dto.Name, userId.Value, DateTime.UtcNow);
        await _repository.AddAsync(list, ct);
        return CreatedAtAction(nameof(Get), new { id = list.Id }, MapToDto(list));
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a shopping list")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var list = await _repository.GetAsync(id, ct);
        if (list is null || list.UserId != userId.Value)
            return NotFound();

        await _repository.DeleteAsync(list, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/items")]
    [SwaggerOperation(Summary = "Add an item to a shopping list")]
    [ProducesResponseType(typeof(ShoppingListItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShoppingListItemDto>> AddItem(Guid id, [FromBody] AddShoppingListItemDto dto, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var command = new AddShoppingListItemCommand
        {
            ShoppingListId = id,
            UserId = userId.Value,
            ProductName = dto.ProductName,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            Category = dto.Category
        };

        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    [SwaggerOperation(Summary = "Remove an item from a shopping list")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveItem(Guid id, Guid itemId, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var list = await _repository.GetAsync(id, ct);
        if (list is null || list.UserId != userId.Value)
            return NotFound();

        list.RemoveItem(itemId);
        await _repository.UpdateAsync(list, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}/items/{itemId:guid}/toggle")]
    [SwaggerOperation(Summary = "Toggle checked state of an item")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ToggleItem(Guid id, Guid itemId, CancellationToken ct)
    {
        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        var list = await _repository.GetAsync(id, ct);
        if (list is null || list.UserId != userId.Value)
            return NotFound();

        list.ToggleItem(itemId);
        await _repository.UpdateAsync(list, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/items/from-image")]
    [SwaggerOperation(Summary = "Extract products from image and add to shopping list")]
    [ProducesResponseType(typeof(IReadOnlyList<ShoppingListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [RequestTimeout(milliseconds: 300_000)]
    public async Task<ActionResult<IReadOnlyList<ShoppingListItemDto>>> AddItemsFromImage(
        Guid id, IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return BadRequest("File is required.");

        var userId = TryGetUserId();
        if (userId is null) return Unauthorized();

        await using var stream = file.OpenReadStream();

        var command = new AddItemsFromImageCommand
        {
            ShoppingListId = id,
            UserId = userId.Value,
            FileStream = stream,
            FileName = file.FileName
        };

        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }    private static ShoppingListDto MapToDto(ShoppingList list) => new(
        list.Id,
        list.Name,
        list.UserId,
        list.CreatedAt,
        list.UpdatedAt,
        list.Items.Select(i => new ShoppingListItemDto(
            i.Id, i.ProductName, i.Quantity, i.Unit, i.Category, i.IsChecked
        )).ToList());
}
