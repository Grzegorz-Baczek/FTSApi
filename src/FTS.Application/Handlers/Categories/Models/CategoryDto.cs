namespace FTS.Application.Handlers.Categories.Models;

public class CategoryDto
{
    public string Name { get; set; }

    public CategoryDto(string name)
    {
        Name = name;
    }
}
