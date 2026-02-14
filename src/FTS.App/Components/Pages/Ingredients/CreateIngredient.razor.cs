using FTS.App.Components.Pages.Ingredients.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Ingredients;

public partial class CreateIngredient
{
    [Inject]
    public IngredientApiClient IngredientApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public IngredientViewModel IngredientViewModel = new IngredientViewModel();

    private async Task CreateIngrednientAsync()
    {
        var createIngredientModel = new CreateIngredientModel
        {
            Name = IngredientViewModel.Name,
            Barcode = IngredientViewModel.Barcode,
            Calories = IngredientViewModel.Calories,
            Carbohydrates = IngredientViewModel.Carbohydrates,
            Proteins = IngredientViewModel.Proteins,
            Fat = IngredientViewModel.Fat,
            SaturatedFat = IngredientViewModel.SaturatedFat,
            Sugars = IngredientViewModel.Sugars,
            Fiber = IngredientViewModel.Fiber,
            Salt = IngredientViewModel.Salt,
        };
        await IngredientApiClient.CreateIngredientAsync(createIngredientModel);
        NavigateToRecipes();
    }

    private void NavigateToRecipes()
    {
        NavigationManager.NavigateTo("/recipes");
    }
}
