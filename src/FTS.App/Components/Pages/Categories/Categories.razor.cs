using FTS.App.Components.Pages.Categories.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Categories;

public partial class Categories : ComponentBase
{
    [Inject]
    public CategoryApiClient CategoryApiClient { get; set; }
    public IEnumerable<CategoryViewModel>? categories;

    protected override async Task OnInitializedAsync()
    {
        categories = await CategoryApiClient.GetCategoriesAsync();
    }
}