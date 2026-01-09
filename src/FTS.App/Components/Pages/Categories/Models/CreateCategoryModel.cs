namespace FTS.App.Components.Pages.Categories.Models;

public class CreateCategoryModel
{
    public string? Name { get; set; }

    public CreateCategoryModel(string? name)
    {
        Name = name;
    }
}
