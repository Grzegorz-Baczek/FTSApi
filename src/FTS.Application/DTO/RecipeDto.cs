namespace FTS.Application.DTO;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Steps { get; set; }
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    //relacje
    public string Author { get; set; }

    public RecipeDto(Guid id, string title, string steps, bool isPublic, string? imageUrl, string author)
    {
        Id = id;
        Title = title;
        Steps = steps;
        IsPublic = isPublic;
        ImageUrl = imageUrl;
        Author = author;
    }
}
