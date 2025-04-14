namespace FTS.Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(Guid id, string? name, Guid categoryId)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
    }

    public static Product Create(string name, Guid CategoryId)
    {
        return new Product(Guid.NewGuid(), name, CategoryId);
    }
}