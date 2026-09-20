using FTS.Core.Entities;
using FTS.Core.Enum;

namespace FTS.UnitTests.Nutrition;

internal static class NutritionAssert
{
    public static void Grams(decimal expected, decimal amount, Unit unit, Ingredient ingredient)
    {
        // Arrange
        var recipeId = Guid.NewGuid();

        // Act
        var actual = RecipeIngredient.Create(amount: amount,
            unit: unit, recipeId: recipeId, ingredient: ingredient).AmountInGrams;

        // Assert: report all conversion inputs when the expected gram amount differs.
        Assert.True(expected == actual,
            $"""
             Gram conversion is incorrect.
               ingredient: {ingredient.Name} ({ingredient.Basis})
               density:    {ingredient.DensityGPerMl?.ToString() ?? "missing"}
               piece weight: {ingredient.GramsPerPiece?.ToString() ?? "missing"}
               input:      {amount} {unit}
               expected:   {expected} g
               actual:     {actual} g
             """);
    }
}
