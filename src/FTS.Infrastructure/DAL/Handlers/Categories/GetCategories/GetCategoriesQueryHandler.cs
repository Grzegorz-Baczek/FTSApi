using FTS.Application.DTO;
using FTS.Application.Handlers.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Categories.GetCategories;

internal sealed class GetCategoriesQueryHandler(FTSDbContext dbContext)
    : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryDto>>
{
    public async Task<IReadOnlyCollection<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categoriesDto = await dbContext.Categories
            .Select(c => new CategoryDto(c.Name))
            .ToListAsync(cancellationToken);

        return categoriesDto;
    }
}