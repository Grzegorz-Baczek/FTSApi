using FTS.App.Components.Pages.Categories.Models;

namespace FTS.App.Components.Pages.Products.Models;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid CategoryId { get; set; }
 }
