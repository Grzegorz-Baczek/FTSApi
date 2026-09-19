﻿using FTS.Core.Enum;
﻿using FTS.Core.Entities;

namespace FTS.Application.Handlers.Recipes.Models;

public class RecipeIngredientDto
{
    public string IngredientName { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountInGrams { get; set; }
    public Unit Unit { get; set; }

    public static RecipeIngredientDto Create(RecipeIngredient r)
        => new RecipeIngredientDto
        {
            IngredientName = r.Ingredient.Name,
            Amount = r.Amount,
            AmountInGrams = r.AmountInGrams,
            Unit = r.Unit
        };

}
