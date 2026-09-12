namespace FTS.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

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