using FTS.App.Components.Pages.ShoppingLists.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FTS.App.Components.Pages.ShoppingLists;

public partial class ShoppingListDetail : ComponentBase
{
    [Parameter] public Guid Id { get; set; }

    [Inject] public ShoppingListApiClient ApiClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private ShoppingListViewModel? _list;
    private bool _loading = true;

    // Formularz ręcznego dodawania
    private string _newItemName = string.Empty;
    private decimal? _newItemQuantity;
    private string? _newItemUnit;
    private string? _newItemCategory;

    // Upload zdjęcia
    private IBrowserFile? _selectedFile;
    private bool _uploading;
    private string? _uploadError;

    protected override async Task OnInitializedAsync()
    {
        await LoadList();
    }

    private async Task LoadList()
    {
        _loading = true;
        try
        {
            _list = await ApiClient.GetAsync(Id);
        }
        catch
        {
            _list = null;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task AddItem()
    {
        if (_list is null || string.IsNullOrWhiteSpace(_newItemName)) return;

        var item = await ApiClient.AddItemAsync(_list.Id, new AddShoppingListItemModel
        {
            ProductName = _newItemName,
            Quantity = _newItemQuantity,
            Unit = _newItemUnit,
            Category = _newItemCategory
        });

        if (item is not null)
        {
            _list.Items.Add(item);
        }

        _newItemName = string.Empty;
        _newItemQuantity = null;
        _newItemUnit = null;
        _newItemCategory = null;
    }

    private async Task ToggleItem(Guid itemId)
    {
        if (_list is null) return;

        await ApiClient.ToggleItemAsync(_list.Id, itemId);
        var item = _list.Items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            item.IsChecked = !item.IsChecked;
        }
    }

    private async Task RemoveItem(Guid itemId)
    {
        if (_list is null) return;

        await ApiClient.RemoveItemAsync(_list.Id, itemId);
        var item = _list.Items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            _list.Items.Remove(item);
        }
    }

    private void OnFileSelected(InputFileChangeEventArgs e)
    {
        _selectedFile = e.File;
        _uploadError = null;
    }

    private async Task UploadImage()
    {
        if (_list is null || _selectedFile is null) return;

        _uploading = true;
        _uploadError = null;

        try
        {
            await using var stream = _selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB
            var items = await ApiClient.AddItemsFromImageAsync(_list.Id, stream, _selectedFile.Name);

            if (items is not null)
            {
                _list.Items.AddRange(items);
            }

            _selectedFile = null;
        }
        catch (Exception ex)
        {
            _uploadError = $"Błąd rozpoznawania: {ex.Message}";
        }
        finally
        {
            _uploading = false;
        }
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/shopping-lists");
    }

    private double GetProgress()
    {
        if (_list is null || _list.Items.Count == 0) return 0;
        return (double)_list.Items.Count(i => i.IsChecked) / _list.Items.Count * 100;
    }
}
