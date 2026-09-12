using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Application.Handlers.Recipes.Queries.GetRecipeById;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Recipes.GetRecipeById;

internal sealed class GetRecipeQueryHandler(FTSDbContext dbContext, 
    IUserRepository userRepository) : IRequestHandler<GetRecipeQuery, RecipeDto>
{
    public async Task<RecipeDto> Handle(GetRecipeQuery query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var recipeDto = await dbContext.Recipes
            .Where(r => r.Id == query.Id && (r.IsPublic || r.Author.Id == userId))
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
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
