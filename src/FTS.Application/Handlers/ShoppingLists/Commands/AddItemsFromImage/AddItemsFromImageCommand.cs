using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.ShoppingLists.Commands.AddItemsFromImage;

public class AddItemsFromImageCommand : IRequest<IReadOnlyList<ShoppingListItemDto>>
{
    public Guid ShoppingListId { get; set; }
    public Guid UserId { get; set; }
    public required Stream FileStream { get; set; }
    public required string FileName { get; set; }
}

internal sealed class AddItemsFromImageCommandHandler(
    IShoppingListRepository repository,
    IOcrService ocrService) : IRequestHandler<AddItemsFromImageCommand, IReadOnlyList<ShoppingListItemDto>>
{
    public async Task<IReadOnlyList<ShoppingListItemDto>> Handle(AddItemsFromImageCommand command, CancellationToken cancellationToken)
    {
        var exists = await repository.ExistsForUserAsync(command.ShoppingListId, command.UserId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Shopping list with id '{command.ShoppingListId}' not found.");
        }

        var products = await ocrService.ExtractProductsAsync(command.FileStream, command.FileName, cancellationToken);

        var existingItems = await repository.GetItemsByShoppingListIdAsync(command.ShoppingListId, cancellationToken);
        var trackingItems = existingItems.ToList();

        var addedItems = new List<ShoppingListItemDto>();
        foreach (var p in products)
        {
            var existing = trackingItems.FirstOrDefault(i =>
                string.Equals(i.ProductName, p.Name, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(i.Unit, p.Unit, StringComparison.OrdinalIgnoreCase));

            if (existing is not null)
            {
                existing.Quantity = (existing.Quantity ?? 0) + (p.Quantity ?? 0);
                await repository.UpdateItemAsync(existing, cancellationToken);
                addedItems.Add(new ShoppingListItemDto(existing.Id, existing.ProductName, existing.Quantity, existing.Unit, existing.Category, existing.IsChecked));
            }
            else
            {
                var item = new ShoppingListItem
                {
                    Id = Guid.NewGuid(),
                    ShoppingListId = command.ShoppingListId,
                    ProductName = p.Name,
                    Quantity = p.Quantity,
                    Unit = p.Unit,
                    Category = p.Category,
                    IsChecked = false
                };

                await repository.AddItemAsync(item, cancellationToken);
                addedItems.Add(new ShoppingListItemDto(item.Id, item.ProductName, item.Quantity, item.Unit, item.Category, item.IsChecked));

                trackingItems.Add(item);
            }
        }

        return addedItems;
    }
}
