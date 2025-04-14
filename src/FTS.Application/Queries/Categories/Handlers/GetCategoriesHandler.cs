using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Queries.Categories.Handlers;

public record GetCategoriesQuery : IRequest<IEnumerable<Category>>;

internal sealed class GetCategoriesHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request,
        CancellationToken ct)
    {
        var categories = await categoryRepository.GetCategoriesAsync(ct);
        return categories;
    }
}