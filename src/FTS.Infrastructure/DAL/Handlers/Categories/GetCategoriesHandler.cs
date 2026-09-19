using FTS.Application.Handlers.Categories.Models;
using FTS.Application.Handlers.Categories.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Categories;

internal sealed class GetCategoriesHandler(FTSDbContext dbContext)
    : IRequestHandler<GetCategories.Query, IReadOnlyCollection<CategoryDto>>
{
    public async Task<IReadOnlyCollection<CategoryDto>> Handle(GetCategories.Query query, CancellationToken cancellationToken)
    {
        var categoriesDto = await dbContext.Categories
            .Select(CategoryDto.AsDto)
            .ToListAsync(cancellationToken);

        return categoriesDto;
    }
}