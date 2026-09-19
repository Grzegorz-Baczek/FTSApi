using System.Linq.Expressions;
using FTS.Core.Entities;
using FTS.Core.Enum;

namespace FTS.Application.Handlers.Ingredients.Models;

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public NutritionBasis Basis { get; set; }
    public decimal? DensityGPerMl { get; set; }
    public decimal? GramsPerPiece { get; set; }
    public decimal Calories { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fat { get; set; }

    public static Expression<Func<Ingredient, IngredientDto>> AsDto =>
        i => new IngredientDto
        {
            Id = i.Id,
            Name = i.Name,
            Basis = i.Basis,
            DensityGPerMl = i.DensityGPerMl,
            GramsPerPiece = i.GramsPerPiece,
            Calories = i.Calories,
            Carbohydrates = i.Carbohydrates,
            Proteins = i.Proteins,
            Fat = i.Fat
        };
}
