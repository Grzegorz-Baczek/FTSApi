using FTS.Core.Entities;
using FTS.Core.Enum;

namespace FTS.UnitTests.Builders;

internal static class Ingredients
{
    public static Ingredient Milk(decimal? density = 1.03m) => Ingredient.Create(
        name: "Milk", calories: 64m, carbohydrates: 4.8m, proteins: 3.3m, fat: 3.2m,
        barcode: null, basis: NutritionBasis.Per100ml, densityGPerMl: density, gramsPerPiece: null,
        saturatedFat: null, sugars: null, fiber: null, salt: null);

    public static Ingredient Flour(decimal? density = 0.53m) => Ingredient.Create(
        name: "Flour", calories: 364m, carbohydrates: 76.3m, proteins: 10.3m, fat: 1m,
        barcode: null, basis: NutritionBasis.Per100g, densityGPerMl: density, gramsPerPiece: null,
        saturatedFat: null, sugars: null, fiber: null, salt: null);

    public static Ingredient Egg(decimal? gramsPerPiece = 58m) => Ingredient.Create(
        name: "Egg", calories: 143m, carbohydrates: 0.7m, proteins: 12.6m, fat: 9.5m,
        barcode: null, basis: NutritionBasis.Per100g, densityGPerMl: null, gramsPerPiece: gramsPerPiece,
        saturatedFat: null, sugars: null, fiber: null, salt: null);

    public static Ingredient Salt() => Ingredient.Create(
        name: "Salt", calories: 0m, carbohydrates: 0m, proteins: 0m, fat: 0m,
        barcode: null, basis: NutritionBasis.Per100g, densityGPerMl: null, gramsPerPiece: null,
        saturatedFat: null, sugars: null, fiber: null, salt: null);
}
