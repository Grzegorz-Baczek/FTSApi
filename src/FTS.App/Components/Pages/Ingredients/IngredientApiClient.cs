using FTS.App.Components.Pages.Ingredients.Models;

namespace FTS.App.Components.Pages.Ingredients;

public class IngredientApiClient(HttpClient HttpClient)
{
    public async Task CreateIngredientAsync(CreateIngredientModel createIngredientModel)
    {
        var response = await HttpClient.PostAsJsonAsync("api/ingredient", createIngredientModel);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new Exception(error?.Reason ?? "Wystąpił nieznany błąd, sprawdź czy pola zostały uzupełnione poprawnie");
        }
    }

    public async Task<IReadOnlyCollection<IngredientListItem>?> GetIngredientsAsync()
    {
        var result = await HttpClient.GetAsync("api/ingredients");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<IReadOnlyCollection<IngredientListItem>?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }

    private record ApiError(string Code, string Reason);
}
