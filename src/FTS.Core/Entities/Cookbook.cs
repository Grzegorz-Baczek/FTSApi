namespace FTS.Core.Entities;

public class Cookbook
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    // relacje
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<CookbookRecipe> CookbookRecipes { get; set; } = new List<CookbookRecipe>();

    public Cookbook(Guid id, string name, Guid userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
    }

    public static Cookbook Create(string name, Guid userId)
    {
        return new Cookbook(Guid.NewGuid(), name, userId);
    }
}
