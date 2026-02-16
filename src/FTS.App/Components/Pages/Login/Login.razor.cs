using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTS.App.Components.Pages.Login;

public partial class Login
{
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public UserApiClient UserApiClient { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private LoginModel LoginViewModel = new LoginModel();

    private async Task HandleLogin()
    {
        var result = await UserApiClient.LoginAsync(LoginViewModel);
        if (result != null)
        {
            Snackbar.Add("You are Login!", Severity.Success);
            NavigationManager.NavigateTo("/recipes", forceLoad: true);
        }
        else
        {
            Snackbar.Add("B³¹d logowania!", Severity.Error);
        }
    }
}