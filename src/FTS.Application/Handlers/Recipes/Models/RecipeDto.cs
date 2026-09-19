using System.Linq.Expressions;
using FTS.Core.Entities;

namespace FTS.Application.Handlers.Recipes.Models;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }

    //relacje
    public string Author { get; set; } = null!;
    public int Servings { get; set; }
    public decimal KcalTotal { get; set; }
    public decimal KcalPerServing { get; set; }
    public decimal CarbohydratesTotal { get; set; }
    public decimal ProteinsTotal { get; set; }
    public decimal FatTotal { get; set; }
    public ICollection<RecipeIngredientDto> RecipeIngredients { get; set; } = new List<RecipeIngredientDto>();

    public static Expression<Func<Recipe, RecipeDto>> AsDto =>
        r => new RecipeDto
        {
            Id = r.Id,
            Title = r.Title,
            Steps = r.Steps,
            IsPublic = r.IsPublic,
            ImageUrl = r.ImageUrl,
            Author = r.Author.Name,
            Servings = r.Servings,
            KcalTotal = r.KcalTotal,
            KcalPerServing = r.KcalPerServing,
            CarbohydratesTotal = r.CarbohydratesTotal,
            ProteinsTotal = r.ProteinsTotal,
            FatTotal = r.FatTotal,
            RecipeIngredients = r.RecipeIngredients.Select(ri => RecipeIngredientDto.Create(ri)).ToList()
        };
}
