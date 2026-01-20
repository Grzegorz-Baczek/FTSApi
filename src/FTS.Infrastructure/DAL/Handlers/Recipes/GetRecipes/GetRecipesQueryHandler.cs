using FTS.Application.DTO;
using FTS.Application.Handlers.Recipes.Queries.GetRecipes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Recipes.GetRecipes;

public sealed class GetRecipesQueryHandler(
    FTSDbContext dbContext) : IRequestHandler<GetRecipesQuery, IEnumerable<RecipeDto>>
{
    public async Task<IEnumerable<RecipeDto>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var recipes =  await dbContext.Recipes
            .Select(r => new RecipeDto(r.Title, r.Steps, r.IsPublic, r.ImageUrl, r.Author.Name))
            .ToListAsync(cancellationToken);

        return recipes;
    }
}
