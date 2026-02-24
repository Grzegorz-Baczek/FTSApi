using FTS.App.Components.Pages.Coobooks.ApiClient;
using FTS.App.Components.Pages.Coobooks.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTS.App.Components.Pages.Coobooks;

public partial class CreateCookbook
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] CookbookApiClient CookbookApiClient { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    public CookbookViewModel CookbookViewModel = new CookbookViewModel();

    private async Task CreateCookbookAsync()
    {
        try
        {
            var createCookbookModel = new CreateCookbookModel(CookbookViewModel.Name);
            await CookbookApiClient.CreateCookbookAsync(createCookbookModel);

            Snackbar.Add("Książkę kucharską dodano pomyślnie!", Severity.Success);
            NavigateToAccount();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private void NavigateToAccount()
    {
        NavigationManager.NavigateTo("/account");
    }
}