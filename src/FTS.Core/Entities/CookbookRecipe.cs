namespace FTS.Core.Entities;

public class CookbookRecipe
{
    public Guid Id { get; set; }
    public DateTime PinnedAt { get; set; }

    //relacje
    public Guid CookbookId { get; set; }
    public Cookbook Cookbook { get; set; } = null!;

    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
}
