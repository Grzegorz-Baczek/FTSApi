using FTS.Core.Entities;
using FTS.Core.Enum;
using FTS.UnitTests.Builders;

namespace FTS.UnitTests.Nutrition;

public class NutritionTests
{
    private readonly Ingredient IngredientSalt;
    private readonly Ingredient IngredientMilk;
    private readonly Ingredient IngredientFlour;

    public NutritionTests()
    {
        IngredientSalt = Ingredients.Salt();
        IngredientMilk = Ingredients.Milk();
        IngredientFlour = Ingredients.Flour();
    }

    [Fact]
    public void Milk_volume_returns_to_milliliters_when_calories_are_calculated()
    {
        // Arrange
        var milk = IngredientMilk;
        var ingredients = new Dictionary<Guid, Ingredient> { [milk.Id] = milk };

        var recipe = CreateRecipe(servings: 1);
        recipe.RecipeIngredients.Add(RecipeIngredient.Create(amount: 200m,
            unit: Unit.Milliliter, recipeId: recipe.Id, ingredient: milk));

        // Act
        recipe.RecalculateNutrition(ingredients);

        // Assert: 206 grams of milk converts back to 200 milliliters and 128 kcal.
        Assert.Equal(128m, recipe.KcalTotal);
    }

    [Fact]
    public void Flour_calories_are_calculated_directly_from_grams()
    {
        // Arrange
        var flour = IngredientFlour;
        var ingredients = new Dictionary<Guid, Ingredient> { [flour.Id] = flour };

        var recipe = CreateRecipe(servings: 1);
        recipe.RecipeIngredients.Add(RecipeIngredient.Create(amount: 1m, 
            unit: Unit.Cup, recipeId: recipe.Id, ingredient: flour));

        // Act
        recipe.RecalculateNutrition(ingredients);

        // Assert: 132.5 grams of flour contributes 482.3 kcal.
        Assert.Equal(482.3m, recipe.KcalTotal);
    }

    [Fact]
    public void A_pinch_of_salt_adds_no_calories()
    {
        // Arrange
        var salt = IngredientSalt;
        var ingredients = new Dictionary<Guid, Ingredient> { [salt.Id] = salt };

        var recipe = CreateRecipe(servings: 1);
        recipe.RecipeIngredients.Add(RecipeIngredient.Create(amount: 1m,
            unit: Unit.Pinch, recipeId: recipe.Id, ingredient: salt));

        // Act
        recipe.RecalculateNutrition(ingredients);

        // Assert: a pinch contributes zero grams and zero calories.
        Assert.Equal(0m, recipe.KcalTotal);
    }

    [Fact]
    public void Kcal_per_serving_is_total_calories_divided_by_servings()
    {
        // Arrange
        var milk = IngredientMilk;
        var ingredients = new Dictionary<Guid, Ingredient> { [milk.Id] = milk };

        var recipe = CreateRecipe(servings: 2);
        recipe.RecipeIngredients.Add(RecipeIngredient.Create(amount: 200m,
            unit: Unit.Milliliter, recipeId: recipe.Id, ingredient: milk));

        // Act
        recipe.RecalculateNutrition(ingredients);

        // Assert: the recipe total remains 128 kcal.
        Assert.Equal(128m, recipe.KcalTotal);
        // Assert: the total is divided by two servings to produce 64 kcal.
        Assert.Equal(64m, recipe.KcalPerServing);
    }

    [Fact]
    public void Change_servings_recalculates_calories_per_serving()
    {
        // Arrange
        var milk = IngredientMilk;
        var ingredients = new Dictionary<Guid, Ingredient> { [milk.Id] = milk };

        var recipe = CreateRecipe(servings: 2);
        recipe.RecipeIngredients.Add(RecipeIngredient.Create(amount: 200m,
            unit: Unit.Milliliter, recipeId: recipe.Id, ingredient: milk));

        // Act
        recipe.RecalculateNutrition(ingredients);
        recipe.ChangeServings(4, ingredients);

        // Assert: the recipe stores the requested number of servings.
        Assert.Equal(4, recipe.Servings);

        // Assert: changing servings does not change the recipe's total calories.
        Assert.Equal(128m, recipe.KcalTotal);

        // Assert: the total calories are now divided across four servings.
        Assert.Equal(32m, recipe.KcalPerServing);
    }

    private static Recipe CreateRecipe(int servings)
    {
        return Recipe.Create(title: "Test recipe", steps: "Mix ingredients.",
            isPublic: true, imageUrl: null, authorId: Guid.NewGuid(), servings: servings);
    }
}
