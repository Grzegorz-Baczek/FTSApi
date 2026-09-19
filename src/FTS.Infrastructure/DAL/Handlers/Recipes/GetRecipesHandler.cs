using FTS.Application.Abstractions;
using FTS.Application.Handlers.Recipes.Models;
using FTS.Application.Handlers.Recipes.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipesHandler(FTSDbContext dbContext,
    ICurrentUser currentUser) : IRequestHandler<GetRecipes.Query, IReadOnlyCollection<RecipeDto>>
{
    public async Task<IReadOnlyCollection<RecipeDto>> Handle(GetRecipes.Query query, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;

        var recipesDto = await dbContext.Recipes
            .Where(r => r.IsPublic || r.AuthorId == userId)
            .Where(r => !query.MaxKcalPerServing.HasValue || r.KcalPerServing <= query.MaxKcalPerServing.Value)
            .Select(RecipeDto.AsDto)
            .ToListAsync(cancellationToken);

        return recipesDto;
    }
}
