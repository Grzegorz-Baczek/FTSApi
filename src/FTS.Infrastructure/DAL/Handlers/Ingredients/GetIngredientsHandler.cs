using FTS.Application.Handlers.Ingredients.Models;
using FTS.Application.Handlers.Ingredients.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Ingredients;

internal sealed class GetIngredientsHandler(FTSDbContext dbContext)
    : IRequestHandler<GetIngredients.Query, IReadOnlyCollection<IngredientDto>>
{
    public async Task<IReadOnlyCollection<IngredientDto>> Handle(
        GetIngredients.Query query,
        CancellationToken cancellationToken)
    {
        var ingredientsDto = await dbContext.Ingredients
            .Select(IngredientDto.AsDto)
            .ToListAsync(cancellationToken);

        return ingredientsDto;
    }
}
