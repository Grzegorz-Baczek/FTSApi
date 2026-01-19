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

    public Recipe() { }

    public Recipe(Guid id, string title, string steps, bool isPublic, string? imageUrl, Guid authorId)
    {
        Id = id;
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        AuthorId = authorId;
    }

    public static Recipe Create(string title, string steps, bool isPublic, string? imageUrl, Guid authorId)
    {
        return new Recipe(Guid.NewGuid(), title, steps, isPublic, imageUrl, authorId);
    }
}
