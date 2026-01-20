namespace FTS.Application.DTO;

public class IngredientDto
{
    public string Name { get; set; }

    public IngredientDto(string name)
    {
        Name = name;
    }
}
