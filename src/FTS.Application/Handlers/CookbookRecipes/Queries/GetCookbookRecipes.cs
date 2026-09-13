using FTS.Application.Handlers.CookbookRecipes.Models;
using MediatR;

namespace FTS.Application.Handlers.CookbookRecipes.Queries;

public static class GetCookbookRecipes
{
    public record Query(Guid CookbookId) : IRequest<IReadOnlyCollection<CookbookRecipeDto>>;
}
