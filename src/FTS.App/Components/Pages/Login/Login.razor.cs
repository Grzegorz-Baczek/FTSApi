using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Login;

public partial class Login
{
    [Inject] public UserApiClient UserApiClient { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    private LoginModel LoginViewModel = new LoginModel();

    private async Task HandleLogin()
    {
        await UserApiClient.LoginAsync(LoginViewModel);

        NavigationManager.NavigateTo("/");
    }
}