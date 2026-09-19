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

        var recipes = dbContext.Recipes
            .Where(r => r.IsPublic || r.AuthorId == userId)
            .Where(r => !query.MaxKcalPerServing.HasValue || r.KcalPerServing <= query.MaxKcalPerServing.Value);

        var recipesDto = await recipes
            .Select(r => new RecipeDto(
                r.Id,
                r.Title,
                r.Steps,
                r.IsPublic,
                r.ImageUrl,
                r.Author.Name,
                r.Servings,
                r.KcalTotal,
                r.KcalPerServing,
                r.CarbohydratesTotal,
                r.ProteinsTotal,
                r.FatTotal,
                r.RecipeIngredients.Select(ri => new RecipeIngredientDto(
                    ri.Ingredient.Name,
                    ri.Amount,
                    ri.AmountInGrams,
                    ri.Unit
                )).ToList()))
            .ToListAsync(cancellationToken);

        return recipesDto;
    }
}
