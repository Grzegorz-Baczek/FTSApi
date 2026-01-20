namespace FTS.Application.DTO;

public class RecipeDto
{
    public string Title { get; set; }
    public string Steps { get; set; }
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    //relacje
    public string Author { get; set; }

    public RecipeDto(string title, string steps, bool isPublic, string? imageUrl, string author)
    {
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        Author = author;
    }
}
