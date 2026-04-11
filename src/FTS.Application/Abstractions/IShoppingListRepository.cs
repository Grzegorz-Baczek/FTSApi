using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IShoppingListRepository
{
    Task<ShoppingList?> GetAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ShoppingList>> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task AddAsync(ShoppingList shoppingList, CancellationToken ct);
    Task UpdateAsync(ShoppingList shoppingList, CancellationToken ct);
    Task DeleteAsync(ShoppingList shoppingList, CancellationToken ct);
}
