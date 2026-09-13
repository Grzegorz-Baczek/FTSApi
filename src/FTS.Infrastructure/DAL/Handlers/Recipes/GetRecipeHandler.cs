using FTS.Application.Abstractions;
using FTS.Application.Handlers.Recipes.Models;
using FTS.Application.Handlers.Recipes.Queries;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipeHandler(
    FTSDbContext dbContext, 
    ICurrentUser currentUser) : IRequestHandler<GetRecipe.Query, RecipeDto>
{
    public async Task<RecipeDto> Handle(GetRecipe.Query query, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;

        var recipeDto = await dbContext.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .Where(r => r.Id == query.Id && (r.IsPublic || r.AuthorId == userId))
            .Select(r => new RecipeDto(
                r.Id,
                r.Title,
                r.Steps,
                r.IsPublic,
                r.ImageUrl,
                r.Author.Name,
                r.RecipeIngredients.Select(ri => new RecipeIngredientDto(
                    ri.Ingredient.Name,
                    ri.Amount,
                    ri.Unit
                )).ToList()))
            .FirstOrDefaultAsync(cancellationToken);
        if (recipeDto == null)
        {
            throw new NotFoundException<Recipe>(query.Id);
        }

        return recipeDto;
    }
}
