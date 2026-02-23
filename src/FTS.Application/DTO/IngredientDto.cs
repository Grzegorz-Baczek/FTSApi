namespace FTS.Application.DTO;

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public IngredientDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
