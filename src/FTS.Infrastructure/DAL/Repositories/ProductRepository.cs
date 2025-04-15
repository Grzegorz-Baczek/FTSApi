using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository(FTSDbContext dbContext) : IProductRepository
{
    public async Task AddProductAsync(Product product, CancellationToken ct)
    {
        await dbContext.Products.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct)
    {
        var products = await dbContext.Products.ToListAsync(ct);
        return products;
    }
}