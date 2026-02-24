using System.Linq.Expressions;
using FTS.App.Components.Pages.Ingredients.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTS.App.Components.Pages.Ingredients;

public partial class CreateIngredient
{
    [Inject]
    public IngredientApiClient IngredientApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public IngredientViewModel IngredientViewModel = new IngredientViewModel();
    [Inject]
    public ISnackbar Snackbar { get; set; }

    private async Task CreateIngrednientAsync()
    {
        try
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
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private void NavigateToRecipes()
    {
        NavigationManager.NavigateTo("/recipes");
    }
}
