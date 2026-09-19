using System.Linq.Expressions;
using FTS.Core.Entities;

namespace FTS.Application.Handlers.CookbookRecipes.Models;

public class CookbookRecipeDto
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public string RecipeTitle { get; set; }
    public DateTime PinnedAt { get; set; }

    public static Expression<Func<CookbookRecipe, CookbookRecipeDto>> AsDto =>
         cbr => new CookbookRecipeDto
         {
             Id = cbr.Id,
             RecipeId = cbr.RecipeId,
             RecipeTitle = cbr.Recipe.Title,
             PinnedAt = cbr.PinnedAt
         };
}
