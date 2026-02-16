using FTS.App.Components.Pages.Recipes.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTS.App.Components.Pages.Recipes;

public partial class CreateRecipe
{
    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;
    [Inject]
    public RecipeApiClient RecipeApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    public RecipeViewModel RecipeViewModel = new RecipeViewModel();

    private async Task CreateRecipeAsync()
    {
        try
        {
            var CreateRecipeModel = new CreateRecipeModel(RecipeViewModel.Title, RecipeViewModel.Steps, RecipeViewModel.ImageUrl);
            await RecipeApiClient.CreateRecipeAsync(CreateRecipeModel);
            Snackbar.Add("Recipe added successfully!", Severity.Success);
            NavigateToRecipes();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error: {ex.Message}", Severity.Error);
        }
    }

    private void NavigateToRecipes()
    {
        NavigationManager.NavigateTo("/recipes");
    }
}