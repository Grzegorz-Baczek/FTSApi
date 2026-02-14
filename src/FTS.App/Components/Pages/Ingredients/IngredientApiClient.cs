using FTS.App.Components.Pages.Ingredients.Models;

namespace FTS.App.Components.Pages.Ingredients;

public class IngredientApiClient(HttpClient HttpClient)
{
    public async Task CreateIngredientAsync(CreateIngredientModel createIngredientModel)
    {
        await HttpClient.PostAsJsonAsync("api/ingredient", createIngredientModel);
    }
}
