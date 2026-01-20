using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Queries.GetRecipes;

public class GetRecipesQuery : IRequest<IEnumerable<RecipeDto>>;
