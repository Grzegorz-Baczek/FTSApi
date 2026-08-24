using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository(FTSDbContext dbContext) :
    BaseRepository<Product>(dbContext), IProductRepository
{
    public async Task<Product?> GetAsync(Guid id, CancellationToken ct)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        return product;
    }
}