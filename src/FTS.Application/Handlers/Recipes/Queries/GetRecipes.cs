using FTS.Application.Handlers.Recipes.Models;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Queries;

public static class GetRecipes
{
    public record Query(decimal? MaxKcalPerServing) : IRequest<IReadOnlyCollection<RecipeDto>>;
}