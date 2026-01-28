using FTS.Application.DTO;
using FTS.Application.Handlers.Ingredients.Queries.GetIngredients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Ingredients.GetIngredients;

internal sealed class GetIngredientsQueryHandler(FTSDbContext dbContext) : IRequestHandler<GetIngredientsQuery, IReadOnlyCollection<IngredientDto>>
{
    public async Task<IReadOnlyCollection<IngredientDto>> Handle(GetIngredientsQuery query, CancellationToken cancellationToken)
    {
        var ingredients = await dbContext.Ingredients
            .Select(i => new IngredientDto(i.Name))
            .ToListAsync(cancellationToken);

        return ingredients;
    }
}
