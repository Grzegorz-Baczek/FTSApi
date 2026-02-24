using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CookbookRepository(FTSDbContext dbContext) : ICookbookRepository
{
    public async Task AddCookbookAsync(Cookbook cookbook, CancellationToken ct)
    {
        await dbContext.Cookbooks.AddAsync(cookbook, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}
