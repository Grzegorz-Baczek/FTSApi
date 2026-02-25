namespace FTS.Application.DTO;

public class CookbookDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public CookbookDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
