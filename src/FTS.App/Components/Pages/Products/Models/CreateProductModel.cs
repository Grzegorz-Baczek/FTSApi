namespace FTS.App.Components.Pages.Products.Models;

public class CreateProductModel
{
    public string? Name { get; set; }
    public Guid? CategoryId { get; set; }

    public CreateProductModel(string? name, Guid? categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
}
