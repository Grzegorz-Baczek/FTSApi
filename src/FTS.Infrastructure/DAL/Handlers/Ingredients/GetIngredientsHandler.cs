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
        var ingredients = await dbContext.Ingredients
            .Select(i => new IngredientDto(i.Id, i.Name, i.Basis, i.DensityGPerMl,
                i.GramsPerPiece, i.Calories, i.Carbohydrates, i.Proteins, i.Fat))
            .ToListAsync(cancellationToken);

        return ingredients;
    }
}
