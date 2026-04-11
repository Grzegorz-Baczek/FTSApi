using FTS.App.Components.Pages.Login.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace FTS.App.Components.Pages.Login;

public partial class Login : IDisposable
{
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public UserApiClient UserApiClient { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IJSRuntime JS { get; set; } = default!;
    [Inject] public IConfiguration Configuration { get; set; } = default!;

    private LoginModel LoginViewModel = new LoginModel();
    private DotNetObjectReference<Login>? _dotNetRef;

    private string GoogleClientId => Configuration["auth:googleClientId"] ?? "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("initGoogleSignIn", _dotNetRef);
        }
    }

    [JSInvokable]
    public async Task OnGoogleSignIn(string idToken)
    {
        var result = await UserApiClient.GoogleLoginAsync(idToken);
        if (result is not null)
        {
            Snackbar.Add("Zalogowano przez Google!", Severity.Success);
            NavigationManager.NavigateTo("/recipes", forceLoad: true);
        }
        else
        {
            Snackbar.Add("Błąd logowania przez Google.", Severity.Error);
        }
    }

    private async Task HandleLogin()
    {
        var result = await UserApiClient.LoginAsync(LoginViewModel);
        if (result != null)
        {
            Snackbar.Add("Zalogowano pomyślnie!", Severity.Success);
            NavigationManager.NavigateTo("/recipes", forceLoad: true);
        }
        else
        {
            Snackbar.Add("Błąd logowania! Sprawdź dane i spróbuj ponownie.", Severity.Error);
        }
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}