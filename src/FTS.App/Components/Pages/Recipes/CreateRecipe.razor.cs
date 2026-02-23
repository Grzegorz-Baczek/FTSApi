using FTS.App.Components.Pages.Ingredients;
using FTS.App.Components.Pages.Ingredients.Models;
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
    public IngredientApiClient IngredientApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    public RecipeViewModel RecipeViewModel = new RecipeViewModel();

    private List<IngredientListItem> _availableIngredients = new();
    private IngredientListItem? _selectedIngredient;
    private decimal _ingredientAmount;
    private string _ingredientUnit = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var ingredients = await IngredientApiClient.GetIngredientsAsync();
            if (ingredients != null)
            {
                _availableIngredients = ingredients.ToList();
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Błąd ładowania składników: {ex.Message}", Severity.Error);
        }
    }

    private async Task<IEnumerable<IngredientListItem>> SearchIngredients(string value, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(value))
            return _availableIngredients;

        return _availableIngredients
            .Where(i => i.Name.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    private void AddIngredient()
    {
        if (_selectedIngredient == null || _ingredientAmount <= 0 || string.IsNullOrWhiteSpace(_ingredientUnit))
            return;

        if (RecipeViewModel.RecipeIngredients.Any(ri => ri.IngredientId == _selectedIngredient.Id))
        {
            Snackbar.Add("Ten składnik został już dodany.", Severity.Warning);
            return;
        }

        RecipeViewModel.RecipeIngredients.Add(new RecipeIngredientViewModel
        {
            IngredientId = _selectedIngredient.Id,
            IngredientName = _selectedIngredient.Name,
            Amount = _ingredientAmount,
            Unit = _ingredientUnit
        });

        _selectedIngredient = null;
        _ingredientAmount = 0;
        _ingredientUnit = string.Empty;
    }

    private void RemoveIngredient(RecipeIngredientViewModel ingredient)
    {
        RecipeViewModel.RecipeIngredients.Remove(ingredient);
    }

    private async Task CreateRecipeAsync()
    {
        try
        {
            var recipeIngredients = RecipeViewModel.RecipeIngredients
                .Select(ri => new CreateRecipeIngredientModel
                {
                    IngredientId = ri.IngredientId,
                    Amount = ri.Amount,
                    Unit = ri.Unit
                })
                .ToList();

            var createRecipeModel = new CreateRecipeModel(
                RecipeViewModel.Title,
                RecipeViewModel.Steps,
                RecipeViewModel.ImageUrl,
                recipeIngredients);

            await RecipeApiClient.CreateRecipeAsync(createRecipeModel);
            Snackbar.Add("Przepis dodano pomyślnie!", Severity.Success);
            NavigateToRecipes();
        }
        catch (Exception ex)
        {
            Snackbar.Add("Błąd, sprawdź czy wszystkie pola zostały dodane poprawnie", Severity.Error);
        }
    }

    private void NavigateToRecipes()
    {
        NavigationManager.NavigateTo("/recipes");
    }
}