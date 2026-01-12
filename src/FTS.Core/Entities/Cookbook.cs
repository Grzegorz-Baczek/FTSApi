namespace FTS.Core.Entities;

public class Cookbook
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; 

    // relacje
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<CookbookRecipe> CookbookRecipes { get; set; } = new List<CookbookRecipe>();
}
