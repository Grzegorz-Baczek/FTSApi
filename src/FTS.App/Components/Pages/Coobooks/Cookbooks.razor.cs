using FTS.App.Components.Pages.Coobooks.ApiClient;
using FTS.App.Components.Pages.Coobooks.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Coobooks;

public partial class Cookbooks : ComponentBase
{
    [Inject]
    public CookbookApiClient CookbookApiClient { get; set; }
    public IEnumerable<CookbookViewModel>? CookbookViewModel;

    protected override async Task OnInitializedAsync()
    {
        CookbookViewModel = await CookbookApiClient.GetCookbookAsync();
    }
}