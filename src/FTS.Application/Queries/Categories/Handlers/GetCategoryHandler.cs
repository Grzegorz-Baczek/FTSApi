using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Queries.Categories.Handlers;

public record GetCategoryQuery(Guid Id) : IRequest<Category>;

internal sealed class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(GetCategoryQuery query, CancellationToken ct)
    {
        var category = await _categoryRepository.GetCategoryAsync(query.Id, ct);

        if (category is null)
        {
            throw new NotFoundCategoryException(query.Id);
        }

        return category;
    }
}
