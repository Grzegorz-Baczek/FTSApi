using FTS.App.Components.Pages.Coobooks.ApiClient;
using FTS.App.Components.Pages.Coobooks.Models;
using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Login;

public partial class Account
{
    private UserViewModel? user;
    private IReadOnlyCollection<CookbookViewModel>? cookbooks;
    private bool isLoading;
    private string? error;
    [Inject] UserApiClient UserApiClient { get; set; }
    [Inject] CookbookApiClient CookbookApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;

        try
        {
            user = await UserApiClient.GetMeAsync();

            if (user is null)
            {
                error = "Nie udało się pobrać danych użytkownika.";
                return;
            }

            cookbooks = await CookbookApiClient.GetCookbookAsync();
        }
        catch (Exception ex)
        {
            error = $"Błąd: {ex.Message}";
        }
        finally
        {
            isLoading = false;
        }
    }

    private static string GetInitial(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "?";

        return name[0].ToString().ToUpper();
    }
}