using FTS.App.Components.Pages.Categories.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Categories;

public partial class CategoryPage : ComponentBase
{
    [Inject]
    public CategoryApiClient CategoryApiClient { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryViewModel? category;

    protected override async Task OnInitializedAsync()
    {
        category = await CategoryApiClient.GetCategoryAsync(CategoryId);
    }
}