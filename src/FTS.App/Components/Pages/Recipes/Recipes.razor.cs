using FTS.App.Components.Pages.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Recipes;

public partial class Recipes : ComponentBase
{
    [Inject]
    public RecipeApiClient RecipeApiClient { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public IEnumerable<RecipeViewModel>? RecipesViewModel;

    protected override async Task OnInitializedAsync()
    {
        RecipesViewModel = await RecipeApiClient.GetRecipesAsync();
    }

    private void NavigateCreateProduct()
    {
        NavigationManager.NavigateTo("/createproduct");
    }

    private void NavigateCreateCategory()
    {
        NavigationManager.NavigateTo("/createcategory");
    }

    private void NavigateToDetails(Guid id)
    {
        NavigationManager.NavigateTo($"/product/{id}");
    }
}