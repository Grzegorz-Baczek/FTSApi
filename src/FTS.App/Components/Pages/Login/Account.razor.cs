using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Login;

public partial class Account
{
    private UserViewModel? user;
    private bool isLoading;
    private string? error;
    [Inject] UserApiClient UserApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;

        try
        {
            user = await UserApiClient.GetMeAsync();

            if (user is null)
                error = "Nie uda³o siê pobraæ danych u¿ytkownika.";
        }
        catch (Exception ex)
        {
            error = $"B³¹d: {ex.Message}";
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