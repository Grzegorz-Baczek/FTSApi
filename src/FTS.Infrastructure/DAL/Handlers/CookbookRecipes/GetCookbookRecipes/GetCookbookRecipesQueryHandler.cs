using FTS.Application.DTO;
using FTS.Application.Handlers.CookbookRecipes.Queries.GetCookbookRecipes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.CookbookRecipes.GetCookbookRecipes;

internal sealed class GetCookbookRecipesQueryHandler(
    FTSDbContext dbContext) : IRequestHandler<GetCookbookRecipesQuery, IReadOnlyCollection<CookbookRecipeDto>>
{
    public async Task<IReadOnlyCollection<CookbookRecipeDto>> Handle(GetCookbookRecipesQuery query, CancellationToken cancellationToken)
    {
        var cookbookRecipes = await dbContext.CookbookRecipes
            .Where(cbr => cbr.CookbookId == query.CookbookId)
            .Select(cbr => new CookbookRecipeDto(cbr.Id, cbr.RecipeId, cbr.Recipe.Title, cbr.PinnedAt))
            .ToListAsync(cancellationToken);

        return cookbookRecipes;
    }
}
