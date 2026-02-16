using FTS.App.Components.Pages.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Recipes;

public partial class Recipes : ComponentBase
{
    [Inject]
    public RecipeApiClient RecipeApiClient { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public IReadOnlyCollection<RecipeViewModel>? RecipesViewModel;

    protected override async Task OnInitializedAsync()
    {
        RecipesViewModel = await RecipeApiClient.GetRecipesAsync();
    }

    private void NavigateCreateRecipe()
    {
        NavigationManager.NavigateTo("/createrecipe");
    }

    private void NavigateCreateIngredient()
    {
        NavigationManager.NavigateTo("/createingredient");
    }

    private void NavigateToDetails(Guid id)
    {
        NavigationManager.NavigateTo($"/recipe/{id}");
    }
}