using FTS.App.Components.Pages.Categories.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Categories;

public partial class CreateCategory
{
    [Inject]
    public CategoryApiClient CategoryApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public CategoryViewModel CategoryViewModel = new CategoryViewModel();

    private async Task CreateCategoryAsync()
    {
        var createCategory = new CreateCategoryModel(CategoryViewModel.Name);
        await CategoryApiClient.CreateCategoryAsync(createCategory);

        NavigateToProducts();
    }

    private void NavigateToProducts()
    {
        NavigationManager.NavigateTo("/products");
    }
}

