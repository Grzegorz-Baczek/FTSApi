using FTS.Core.Entities;
using FTS.Core.Enum;
using FTS.Core.Exceptions;

namespace FTS.UnitTests.Domain;

public class IngredientCreateTests
{
    [Fact]
    public void Per100ml_ingredient_without_density_throws_domain_exception()
    {
        // Arrange
        var action = () => Ingredient.Create(name: "Milk", calories: 64m, 
            carbohydrates: 4.8m, proteins: 3.3m, fat: 3.2m, barcode: null,
            basis: NutritionBasis.Per100ml, densityGPerMl: null, gramsPerPiece: null,
            saturatedFat: null, sugars: null, fiber: null, salt: null);

        // Assert: creating the invalid ingredient throws a domain exception.
        Assert.Throws<DomainException>(action);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Non_positive_density_throws_domain_exception(decimal density)
    {
        // Arrange
        var action = () => Ingredient.Create(name: "Milk", calories: 64m, carbohydrates: 4.8m, 
            proteins: 3.3m, fat: 3.2m, barcode: null, basis: NutritionBasis.Per100ml,
            densityGPerMl: density, gramsPerPiece: null, saturatedFat: null, sugars: null, 
            fiber: null, salt: null);

        // Assert: the domain rejects a non-positive density.
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Empty_name_throws_domain_exception()
    {
        // Arrange
        var action = () => Ingredient.Create(
            name: "", calories: 0m, carbohydrates: 0m, proteins: 0m, fat: 0m,
            barcode: null, basis: NutritionBasis.Per100g, densityGPerMl: null, gramsPerPiece: null,
            saturatedFat: null, sugars: null, fiber: null, salt: null);

        // Assert: the domain rejects an empty ingredient name.
        Assert.Throws<DomainException>(action);
    }
}
