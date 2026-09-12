namespace FTS.Application.DTO;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    //relacje
    public string Author { get; set; } = null!;
    public ICollection<RecipeIngredientDto> RecipeIngredients { get; set; } = new List<RecipeIngredientDto>();

    public RecipeDto(Guid id, string title, string steps, bool isPublic, 
        string? imageUrl, string author, ICollection<RecipeIngredientDto> recipeIngredients)
    {
        Id = id;
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        Author = author;
        RecipeIngredients = recipeIngredients;
    }
}
