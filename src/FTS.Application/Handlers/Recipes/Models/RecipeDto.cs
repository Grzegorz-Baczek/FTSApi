namespace FTS.Application.Handlers.Recipes.Models;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    //relacje
    public string Author { get; set; } = null!;
    public int Servings { get; set; }
    public decimal KcalTotal { get; set; }
    public decimal KcalPerServing { get; set; }
    public decimal CarbohydratesTotal { get; set; }
    public decimal ProteinsTotal { get; set; }
    public decimal FatTotal { get; set; }
    public ICollection<RecipeIngredientDto> RecipeIngredients { get; set; } = new List<RecipeIngredientDto>();

    public RecipeDto(Guid id, string title, string steps, bool isPublic, 
        string? imageUrl, string author, int servings, decimal kcalTotal, decimal kcalPerServing,
        decimal carbohydratesTotal, decimal proteinsTotal, decimal fatTotal,
        ICollection<RecipeIngredientDto> recipeIngredients)
    {
        Id = id;
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        Author = author;
        Servings = servings;
        KcalTotal = kcalTotal;
        KcalPerServing = kcalPerServing;
        CarbohydratesTotal = carbohydratesTotal;
        ProteinsTotal = proteinsTotal;
        FatTotal = fatTotal;
        RecipeIngredients = recipeIngredients;
    }
}
