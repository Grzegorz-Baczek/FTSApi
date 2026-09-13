using FTS.Application.Abstractions;
using FTS.Application.Handlers.CookbookRecipes.Models;
using FTS.Application.Handlers.CookbookRecipes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.CookbookRecipes;

internal sealed class GetCookbookRecipesHandler(FTSDbContext dbContext, IUserRepository userRepository)
    : IRequestHandler<GetCookbookRecipes.Query, IReadOnlyCollection<CookbookRecipeDto>>
{
    public async Task<IReadOnlyCollection<CookbookRecipeDto>> Handle(GetCookbookRecipes.Query query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var cookbookRecipes = await dbContext.CookbookRecipes
            .Where(cbr => cbr.CookbookId == query.CookbookId && cbr.Cookbook.UserId == userId)
            .Select(cbr => new CookbookRecipeDto(cbr.Id, cbr.RecipeId, cbr.Recipe.Title, cbr.PinnedAt))
            .ToListAsync(cancellationToken);

        return cookbookRecipes;
    }
}
