using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IShoppingListRepository
{
    Task<ShoppingList?> GetAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ShoppingList>> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task<bool> ExistsForUserAsync(Guid id, Guid userId, CancellationToken ct);
    Task AddAsync(ShoppingList shoppingList, CancellationToken ct);
    Task AddItemAsync(ShoppingListItem item, CancellationToken ct);
    Task<IReadOnlyList<ShoppingListItem>> GetItemsByShoppingListIdAsync(Guid shoppingListId, CancellationToken ct);
    Task UpdateItemAsync(ShoppingListItem item, CancellationToken ct);
    Task UpdateAsync(ShoppingList shoppingList, CancellationToken ct);
    Task DeleteAsync(ShoppingList shoppingList, CancellationToken ct);
}
