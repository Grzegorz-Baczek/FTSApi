using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Queries.GetIngredients;

public class GetIngredientsQuery : IRequest<IEnumerable<IngredientDto>>;

