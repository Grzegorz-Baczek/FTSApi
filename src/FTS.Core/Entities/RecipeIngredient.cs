using FTS.Core.Enum;
using FTS.Core.Exceptions;

namespace FTS.Core.Entities;

public class RecipeIngredient
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountInGrams { get; set; }
    public Unit Unit { get; set; }

    //relacje
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;

    private RecipeIngredient()
    { }

    private RecipeIngredient(Guid id,
        decimal amount, Unit unit, decimal amountInGrams, Guid recipeId, Guid ingredientId)
    {
        Id = id;
        Amount = amount;
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

        return new RecipeIngredient(Guid.NewGuid(), amount, unit, grams, recipeId, ingredient.Id)
        {
            Ingredient = ingredient
        };
    }

    private static decimal ToGrams(decimal amount, Unit unit, Ingredient ingredient)
        => unit switch
        {
            Unit.Pinch or Unit.ToTaste => 0m,

            Unit.Gram => amount,
            Unit.Kilogram => amount * 1000m,

            Unit.Piece => amount * (ingredient.GramsPerPiece
                               ?? throw new MissingPieceWeightException(ingredient.Name)),

            Unit.Milliliter => amount * RequireDensity(ingredient),
            Unit.Liter => amount * 1000m * RequireDensity(ingredient),
            Unit.Teaspoon => amount * 5m * RequireDensity(ingredient),
            Unit.Tablespoon => amount * 15m * RequireDensity(ingredient),
            Unit.Cup => amount * 250m * RequireDensity(ingredient),

            _ => throw new DomainException($"Unsupported unit '{unit}'.")
        };

    private static decimal RequireDensity(Ingredient ingredient)
    {
        if (!ingredient.DensityGPerMl.HasValue)
        {
            throw new MissingDensityException(ingredient.Name);
        }

        return ingredient.DensityGPerMl.Value;
    }
}
