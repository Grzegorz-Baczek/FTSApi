namespace FTS.App.Components.Pages.Coobooks.Models;

public class CreateCookbookModel
{
    public string Name { get; set; }

    public CreateCookbookModel(string name)
    {
        Name = name;
    }
}