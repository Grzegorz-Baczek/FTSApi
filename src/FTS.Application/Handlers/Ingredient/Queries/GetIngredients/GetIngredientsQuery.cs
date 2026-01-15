using FTS.Application.Abstractions;
using MediatR;

namespace FTS.Application.Handlers.Ingredient.Queries.GetIngredients;

public class GetIngredientsQuery : IRequest<IEnumerable<Core.Entities.Ingredient>>;

internal sealed class GetIngredientsQueryHandler(IIngredientRepository ingredientRepository) : IRequestHandler<GetIngredientsQuery, IEnumerable<Core.Entities.Ingredient>>
{
    public async Task<IEnumerable<Core.Entities.Ingredient>> Handle(GetIngredientsQuery query, CancellationToken cancellationToken)
    {
        var ingredients = await ingredientRepository.GetIngredientsAsync(cancellationToken);
        return ingredients;
    }
}
