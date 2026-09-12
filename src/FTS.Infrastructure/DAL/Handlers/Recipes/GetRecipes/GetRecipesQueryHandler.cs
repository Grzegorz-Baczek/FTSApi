using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Application.Handlers.Recipes.Queries.GetRecipes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Recipes.GetRecipes;

public sealed class GetRecipesQueryHandler(FTSDbContext dbContext, 
    IUserRepository userRepository) : IRequestHandler<GetRecipesQuery, IReadOnlyCollection<RecipeDto>>
{
    public async Task<IReadOnlyCollection<RecipeDto>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var recipesDto = await dbContext.Recipes
            .Where(r => r.IsPublic || r.AuthorId == userId)
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
            .ToListAsync(cancellationToken);

        return recipesDto;
    }
}
