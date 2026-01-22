using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Queries.GetRecipeById;

public record GetRecipeQuery(Guid Id) : IRequest<RecipeDto>;
