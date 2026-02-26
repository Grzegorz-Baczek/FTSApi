using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.CookbookRecipes.Queries.GetCookbookRecipes;

public record GetCookbookRecipesQuery(Guid CookbookId) : IRequest<IReadOnlyCollection<CookbookRecipeDto>>;