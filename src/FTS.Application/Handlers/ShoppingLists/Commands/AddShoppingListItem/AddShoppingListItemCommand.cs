using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Core.Entities;
using FluentValidation;
using MediatR;

namespace FTS.Application.Handlers.ShoppingLists.Commands.AddShoppingListItem;

public class AddShoppingListItemCommand : IRequest<ShoppingListItemDto>
{
    public Guid ShoppingListId { get; set; }
    public Guid UserId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
}

internal sealed class AddShoppingListItemCommandHandler(
    IShoppingListRepository repository,
    IValidator<AddShoppingListItemCommand> validator) : IRequestHandler<AddShoppingListItemCommand, ShoppingListItemDto>
{
    public async Task<ShoppingListItemDto> Handle(AddShoppingListItemCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var exists = await repository.ExistsForUserAsync(command.ShoppingListId, command.UserId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Shopping list with id '{command.ShoppingListId}' not found.");
        }

        // Sprawdź czy istnieje item z taką samą nazwą i jednostką — jeśli tak, zwiększ quantity
        var existingItems = await repository.GetItemsByShoppingListIdAsync(command.ShoppingListId, cancellationToken);
        var duplicate = existingItems.FirstOrDefault(i =>
            string.Equals(i.ProductName, command.ProductName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.Unit, command.Unit, StringComparison.OrdinalIgnoreCase));

        if (duplicate is not null)
        {
            duplicate.Quantity = (duplicate.Quantity ?? 0) + (command.Quantity ?? 0);
            await repository.UpdateItemAsync(duplicate, cancellationToken);
            return new ShoppingListItemDto(duplicate.Id, duplicate.ProductName, duplicate.Quantity, duplicate.Unit, duplicate.Category, duplicate.IsChecked);
        }

        var item = new ShoppingListItem
        {
            Id = Guid.NewGuid(),
            ShoppingListId = command.ShoppingListId,
            ProductName = command.ProductName,
            Quantity = command.Quantity,
            Unit = command.Unit,
            Category = command.Category,
            IsChecked = false
        };

        await repository.AddItemAsync(item, cancellationToken);

        return new ShoppingListItemDto(item.Id, item.ProductName, item.Quantity, item.Unit, item.Category, item.IsChecked);
    }
}
