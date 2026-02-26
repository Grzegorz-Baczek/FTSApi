namespace FTS.Application.DTO;

public class CookbookRecipeDto
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public string RecipeTitle { get; set; }
    public DateTime PinnedAt { get; set; }

    public CookbookRecipeDto(Guid id, Guid recipeId, string recipeTitle, DateTime pinnedAt)
    {
        Id = id;
        RecipeId = recipeId;
        RecipeTitle = recipeTitle;
        PinnedAt = pinnedAt;
    }
}
