using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTS.App.Components.Pages.Login;

public partial class Register
{
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public UserApiClient UserApiClient { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private RegisterModel RegisterViewModel = new();

    private async Task HandleRegister()
    {
        var success = await UserApiClient.RegisterAsync(RegisterViewModel);
        if (success)
        {
            Snackbar.Add("Rejestracja zakoñczona pomyœlnie! Mo¿esz siê zalogowaæ.", Severity.Success);
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            Snackbar.Add("B³¹d rejestracji! Spróbuj ponownie.", Severity.Error);
        }
    }
}
