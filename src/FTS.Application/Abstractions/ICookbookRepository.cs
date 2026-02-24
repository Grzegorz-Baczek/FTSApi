using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface ICookbookRepository
{
    Task AddCookbookAsync(Cookbook cookbook, CancellationToken ct);
}
