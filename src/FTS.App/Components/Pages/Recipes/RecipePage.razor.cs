using FTS.App.Components.Pages.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Recipes;

public partial class RecipePage : ComponentBase
{
    [Inject]
    public RecipeApiClient RecipeApiClient { get; set; } = default!;

    [Parameter]
    public Guid Id { get; set; }

    public RecipeViewModel? recipe;

    protected override async Task OnInitializedAsync()
    {
        recipe = await RecipeApiClient.GetRecipeAsync(Id);
    }
}