using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetAsync(Guid id, CancellationToken ct);
}