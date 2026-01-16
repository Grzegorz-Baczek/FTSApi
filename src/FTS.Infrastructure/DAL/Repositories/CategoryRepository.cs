using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CategoryRepository(FTSDbContext dbContext) : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category, CancellationToken ct)
    {
        await dbContext.Categories.AddAsync(category, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await dbContext.Categories.ToListAsync(ct);
        return categories;
    }
}