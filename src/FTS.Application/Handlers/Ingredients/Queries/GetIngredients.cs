using FTS.Application.Handlers.Ingredients.Models;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Queries;

public static class GetIngredients
{
    public record Query : IRequest<IReadOnlyCollection<IngredientDto>>;
}

