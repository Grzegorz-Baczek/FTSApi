using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Queries.GetIngredients;


public class GetIngredientsQuery : IRequest<IEnumerable<Ingredient>>;

internal sealed class GetIngredientsQueryHandler(IIngredientRepository ingredientRepository) : IRequestHandler<GetIngredientsQuery, IEnumerable<Ingredient>>
{
    public async Task<IEnumerable<Ingredient>> Handle(GetIngredientsQuery query, CancellationToken cancellationToken)
    {
        var ingredients = await ingredientRepository.GetIngredientsAsync(cancellationToken);
        return ingredients;
    }
}
