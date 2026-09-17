using FTS.Core.Enum;
using FTS.Core.Exceptions;

namespace FTS.Core.Entities;

public class RecipeIngredient
{
    public Guid Id { get; set; }
    public decimal AmountInGrams { get; set; }
    public Unit Unit { get; set; }

    //relacje
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;

    public RecipeIngredient(Guid id, decimal amountInGrams, Unit unit, Guid recipeId, Guid ingredientId)
    {
        Id = id;
        AmountInGrams = amountInGrams;
        Unit = unit;
        RecipeId = recipeId;
        IngredientId = ingredientId;

    }

    public static RecipeIngredient Create(decimal amount, Unit unit, Guid recipeId, Ingredient ingredient)
    {
        if (amount <= 0)
        {
            throw new DomainException("Ingredient amount must be greater than zero.");
        }

        var grams = ToGrams(amount, unit, ingredient);
        return new RecipeIngredient(Guid.NewGuid(), grams, unit, recipeId, ingredient.Id)
        {
            Ingredient = ingredient
        };
    }

    private static decimal ToGrams(decimal amount, Unit unit, Ingredient ingredient)
    {
        if (unit is Unit.Pinch or Unit.ToTaste)
        {
            return 0;
        }

        if (unit == Unit.Piece)
        {
            if (!ingredient.GramsPerPiece.HasValue)
            {
                throw new MissingPieceWeightException(ingredient.Name);
            }

            return amount * ingredient.GramsPerPiece.Value;
        }

        var milliliters = unit switch
        {
            Unit.Gram => amount,
            Unit.Kilogram => amount * 1000m,
            Unit.Milliliter => amount,
            Unit.Liter => amount * 1000m,
            Unit.Teaspoon => amount * 5m,
            Unit.Tablespoon => amount * 15m,
            Unit.Cup => amount * 250m,
            _ => throw new DomainException($"Unsupported unit '{unit}'.")
        };

        if (ingredient.Basis == NutritionBasis.Per100ml)
        {
            RequireDensity(ingredient);
        }

        if (ingredient.Basis == NutritionBasis.Per100g)
        {
            return unit is Unit.Gram or Unit.Kilogram
                ? milliliters
                : RequireDensity(ingredient) * milliliters;
        }

        return RequireDensity(ingredient) * milliliters;
    }

    private static decimal RequireDensity(Ingredient ingredient)
    {
        if (!ingredient.DensityGPerMl.HasValue)
        {
            throw new MissingDensityException(ingredient.Name);
        }

        return ingredient.DensityGPerMl.Value;
    }
}
