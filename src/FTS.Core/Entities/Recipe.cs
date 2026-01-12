namespace FTS.Core.Entities;

public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    // relacje
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public ICollection<CookbookRecipe> CookbookRecipes { get; set; } = new List<CookbookRecipe>();
    public ICollection<PointsLog> PointsLogs { get; set; } = new List<PointsLog>();
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
