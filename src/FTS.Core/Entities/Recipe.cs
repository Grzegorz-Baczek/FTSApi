namespace FTS.Core.Entities;

using FTS.Core.Enum;
using FTS.Core.Exceptions;

public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Servings { get; set; }
    public decimal KcalTotal { get; private set; }
    public decimal KcalPerServing { get; private set; }
    public decimal CarbohydratesTotal { get; private set; }
    public decimal ProteinsTotal { get; private set; }
    public decimal FatTotal { get; private set; }

    // relacje
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public ICollection<CookbookRecipe> CookbookRecipes { get; set; } = new List<CookbookRecipe>();
    public ICollection<PointsLog> PointsLogs { get; set; } = new List<PointsLog>();
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public Recipe() { }

    public Recipe(Guid id, string title, string steps, bool isPublic, string? imageUrl, Guid authorId, int servings)
    {
        Id = id;
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        AuthorId = authorId;
        Servings = servings;
    }

    public static Recipe Create(string title, string steps, bool isPublic, string? imageUrl, Guid authorId, int servings)
    {
        if (servings <= 0)
        {
            throw new DomainException("Recipe servings must be greater than zero.");
        }

        return new Recipe(Guid.NewGuid(), title, steps, isPublic, imageUrl, authorId, servings);
    }

    public void RecalculateNutrition(IReadOnlyDictionary<Guid, Ingredient> ingredients)
    {
        KcalTotal = 0;
        CarbohydratesTotal = 0;
        ProteinsTotal = 0;
        FatTotal = 0;

        foreach (var recipeIngredient in RecipeIngredients)
        {
            if (!ingredients.TryGetValue(recipeIngredient.IngredientId, out var ingredient))
            {
                throw new DomainException($"Ingredient '{recipeIngredient.IngredientId}' was not found.");
            }

            var nutritionAmount = recipeIngredient.AmountInGrams == 0
                ? 0
                : ingredient.Basis == NutritionBasis.Per100g
                    ? recipeIngredient.AmountInGrams
                    : recipeIngredient.AmountInGrams / ingredient.DensityGPerMl!.Value;
            var factor = nutritionAmount / 100m;

            KcalTotal += ingredient.Calories * factor;
            CarbohydratesTotal += ingredient.Carbohydrates * factor;
            ProteinsTotal += ingredient.Proteins * factor;
            FatTotal += ingredient.Fat * factor;
        }

        KcalPerServing = KcalTotal / Servings;
    }
}
