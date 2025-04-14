namespace FTS.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<Product> Products { get; set; } = new List<Product>();

    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(string name)
    {
        return new Category(Guid.NewGuid(), name);
    }
}