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

    public IngredientDto(Guid id, string name, NutritionBasis basis, decimal? densityGPerMl,
        decimal? gramsPerPiece, decimal calories, decimal carbohydrates, decimal proteins, decimal fat)
    {
        Id = id;
        Name = name;
        Basis = basis;
        DensityGPerMl = densityGPerMl;
        GramsPerPiece = gramsPerPiece;
        Calories = calories;
        Carbohydrates = carbohydrates;
        Proteins = proteins;
        Fat = fat;
    }
}
