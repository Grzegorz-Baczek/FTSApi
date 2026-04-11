using FTS.App.Components.Pages.ShoppingLists.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.ShoppingLists;

public partial class ShoppingLists : ComponentBase
{
    [Inject] public ShoppingListApiClient ApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private List<ShoppingListViewModel>? _lists;
    private bool _loading = true;
    private bool _showCreateDialog;
    private string _newListName = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadLists();
    }

    private async Task LoadLists()
    {
        _loading = true;
        try
        {
            _lists = await ApiClient.GetAllAsync();
        }
        catch
        {
            _lists = [];
        }
        finally
        {
            _loading = false;
        }
    }

    private void OpenCreateDialog()
    {
        _newListName = string.Empty;
        _showCreateDialog = true;
    }

    private async Task CreateList()
    {
        if (string.IsNullOrWhiteSpace(_newListName)) return;

        var created = await ApiClient.CreateAsync(new CreateShoppingListModel { Name = _newListName });
        _showCreateDialog = false;

        if (created is not null)
        {
            NavigationManager.NavigateTo($"/shopping-lists/{created.Id}");
        }
    }

    private async Task DeleteList(Guid id)
    {
        await ApiClient.DeleteAsync(id);
        await LoadLists();
    }

    private void NavigateToList(Guid id)
    {
        NavigationManager.NavigateTo($"/shopping-lists/{id}");
    }

    private static double GetProgress(ShoppingListViewModel list)
    {
        if (list.Items.Count == 0) return 0;
        return (double)list.Items.Count(i => i.IsChecked) / list.Items.Count * 100;
    }
}
