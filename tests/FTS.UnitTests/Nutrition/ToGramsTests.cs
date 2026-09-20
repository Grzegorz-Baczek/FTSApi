using FTS.Core.Entities;
using FTS.Core.Enum;
using FTS.Core.Exceptions;
using FTS.UnitTests.Builders;

namespace FTS.UnitTests.Nutrition;

public class ToGramsTests
{
    private readonly Ingredient IngredientSalt;
    private readonly Ingredient IngredientMilk;
    private readonly Ingredient IngredientFlour;
    private readonly Ingredient IngredientEgg;
    private readonly Guid Id;

    public ToGramsTests()
    {
        IngredientSalt = Ingredients.Salt();
        IngredientMilk = Ingredients.Milk();
        IngredientFlour = Ingredients.Flour();
        IngredientEgg = Ingredients.Egg();
        Id = Guid.NewGuid();
    }

    [Theory]
    [InlineData(100, Unit.Gram, 100)]
    [InlineData(1, Unit.Kilogram, 1000)]
    public void Mass_units_are_converted_to_grams(decimal amount, Unit unit, decimal expected)
    {
        // Arrange
        var ingredient = IngredientSalt;

        // Act

        // Assert: NutritionAssert verifies the calculated gram amount.
        NutritionAssert.Grams(expected: expected, amount: amount, unit: unit, ingredient: ingredient);
    }

    [Theory]
    [InlineData(1, Unit.Cup, 132.5)]
    [InlineData(1, Unit.Teaspoon, 2.65)]
    public void Flour_volume_units_use_density(decimal amount, Unit unit, decimal expected)
    {
        // Arrange
        var ingredient = IngredientFlour;

        // Act

        // Assert: NutritionAssert verifies the calculated gram amount.
        NutritionAssert.Grams(expected: expected, amount: amount, unit: unit, ingredient: ingredient);
    }

    [Theory]
    [InlineData(200, Unit.Milliliter, 206)]
    [InlineData(1, Unit.Cup, 257.5)]
    [InlineData(1, Unit.Tablespoon, 15.45)]
    public void Milk_volume_units_use_density(decimal amount, Unit unit, decimal expected)
    {
        // Arrange
        var ingredient = IngredientMilk;

        // Act

        // Assert: NutritionAssert verifies the calculated gram amount.
        NutritionAssert.Grams(expected: expected, amount: amount, unit: unit, ingredient: ingredient);
    }

    [Fact]
    public void Milk_mass_units_ignore_density()
    {
        // Arrange
        var ingredient = IngredientMilk;

        // Act

        // Assert: NutritionAssert verifies that 200 grams remains 200 grams.
        NutritionAssert.Grams(expected: 200m, amount: 200m, unit: Unit.Gram, ingredient: ingredient);
    }

    [Fact]
    public void Pieces_use_grams_per_piece()
    {
        // Arrange
        var ingredient = IngredientEgg;

        // Act

        // Assert: NutritionAssert verifies that two eggs weigh 116 grams.
        NutritionAssert.Grams(expected: 116m, amount: 2m, unit: Unit.Piece, ingredient: ingredient);
    }

    [Theory]
    [InlineData(Unit.Pinch)]
    [InlineData(Unit.ToTaste)]
    public void Negligible_units_return_zero_without_density(Unit unit)
    {
        // Arrange
        var ingredient = IngredientSalt;

        // Act

        // Assert: NutritionAssert verifies that the unit contributes zero grams.
        NutritionAssert.Grams(expected: 0m, amount: 1m, unit: unit, ingredient: ingredient);
    }

    [Fact]
    public void Volume_without_density_throws_missing_density_exception()
    {
        // Arrange
        var ingredient = Ingredients.Flour(density: null);
        var recipeId = Id;

        // Act
        var action = () => RecipeIngredient.Create(amount: 1m, 
            unit: Unit.Cup, recipeId: recipeId, ingredient: ingredient);

        // Assert: volume conversion rejects ingredients without density.
        Assert.Throws<MissingDensityException>(action);
    }

    [Fact]
    public void Piece_without_weight_throws_missing_piece_weight_exception()
    {
        // Arrange
        var ingredient = Ingredients.Egg(gramsPerPiece: null);
        var recipeId = Id;

        // Act
        var action = () => RecipeIngredient.Create( amount: 1m,
            unit: Unit.Piece, recipeId: recipeId, ingredient: ingredient);

        // Assert: piece conversion rejects ingredients without piece weight.
        Assert.Throws<MissingPieceWeightException>(action);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Non_positive_amount_throws_domain_exception(decimal amount)
    {
        // Arrange
        var ingredient = IngredientSalt;
        var recipeId = Id;

        // Act
        var action = () => RecipeIngredient.Create(amount: amount,
            unit: Unit.Gram, recipeId: recipeId, ingredient: ingredient);

        // Assert: the domain rejects zero and negative amounts.
        Assert.Throws<DomainException>(action);
    }
}
