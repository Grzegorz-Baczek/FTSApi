using FTS.Core.Exceptions;
using FTS.Core.Enum;

namespace FTS.Core.Entities;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    //kod kreskowy
    public string? Barcode { get; set; }

    public NutritionBasis Basis { get; set; }
    public decimal? DensityGPerMl { get; set; }
    public decimal? GramsPerPiece { get; set; }

    public decimal Calories { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fat { get; set; }

    // W tym kwasy tłuszczowe nasycone
    public decimal? SaturatedFat { get; set; }
    public decimal? Sugars { get; set; }

    // Błonnik
    public decimal? Fiber { get; set; }
    public decimal? Salt { get; set; }

    //relacje
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    private Ingredient(Guid id,
        string name, decimal calories, decimal carbohydrates, decimal proteins, decimal fat,
        string? barcode, NutritionBasis basis, decimal? densityGPerMl, decimal? gramsPerPiece,
        decimal? saturatedFat, decimal? sugars, decimal? fiber, decimal? salt)
    {
        Id = id;
        Name = name;
        Calories = calories;
        Carbohydrates = carbohydrates;
        Proteins = proteins;
        Fat = fat;
        Barcode = barcode;
        Basis = basis;
        DensityGPerMl = densityGPerMl;
        GramsPerPiece = gramsPerPiece;
        SaturatedFat = saturatedFat;
        Sugars = sugars;
        Fiber = fiber;
        Salt = salt;
    }

    public static Ingredient Create(string name, decimal calories, decimal carbohydrates, decimal proteins,
        decimal fat, string? barcode, NutritionBasis basis, decimal? densityGPerMl, decimal? gramsPerPiece,
        decimal? saturatedFat, decimal? sugars, decimal? fiber, decimal? salt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Ingredient name cannot be empty.");
        }

        if (basis == NutritionBasis.Per100ml && densityGPerMl is null or <= 0)
        {
            throw new DomainException(
                $"Składnik '{name}' ma wartości odżywcze na 100 ml, więc wymaga podanej gęstości.");
        }

        if (densityGPerMl is <= 0)
        {
            throw new DomainException($"Gęstość składnika '{name}' musi być większa od zera.");
        }

        if (gramsPerPiece is <= 0)
        {
            throw new DomainException($"Waga sztuki składnika '{name}' musi być większa od zera.");
        }

        return new Ingredient(Guid.NewGuid(), name, calories,
            carbohydrates, proteins, fat, barcode, basis, densityGPerMl, gramsPerPiece,
            saturatedFat, sugars, fiber, salt);
    }
}
