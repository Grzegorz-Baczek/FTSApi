using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class ShoppingListRepository(FTSDbContext dbContext) : IShoppingListRepository
{
    public async Task<ShoppingList?> GetAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.ShoppingLists
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<IReadOnlyList<ShoppingList>> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await dbContext.ShoppingLists
            .Include(s => s.Items)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(ShoppingList shoppingList, CancellationToken ct)
    {
        await dbContext.ShoppingLists.AddAsync(shoppingList, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(ShoppingList shoppingList, CancellationToken ct)
    {
        // Entity framework tracks the aggregate root implicitly, but when we just add to the collection
        // of a tracked entity, we don't necessarily want caller to explicitly call Update on the root 
        // as EF might try to update unchanged fields or try to attach already tracking items in a conflicting way.
        // Instead we can just save changes since the context tracks the ShoppingList and its Items:
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(ShoppingList shoppingList, CancellationToken ct)
    {
        dbContext.ShoppingLists.Remove(shoppingList);
        await dbContext.SaveChangesAsync(ct);
    }
}
