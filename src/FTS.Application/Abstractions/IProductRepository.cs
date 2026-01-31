using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IProductRepository
{
    Task DeleteProductAsync(Product product, CancellationToken ct);
    Task<Product?> GetProductAsync(Guid id, CancellationToken ct);
}