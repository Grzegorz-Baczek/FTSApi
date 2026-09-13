using FTS.Application.Handlers.Recipes.Models;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Queries;

public static class GetRecipe 
{
    public record Query(Guid Id) : IRequest<RecipeDto>;
}
