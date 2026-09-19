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
            .Where(r => r.Id == query.Id && (r.IsPublic || r.AuthorId == userId))
            .Select(RecipeDto.AsDto)
            .FirstOrDefaultAsync(cancellationToken);
        if (recipeDto == null)
        {
            throw new NotFoundException<Recipe>(query.Id);
        }

        return recipeDto;
    }
}
