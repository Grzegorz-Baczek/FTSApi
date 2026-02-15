using FTS.Core.Enum;

namespace FTS.Application.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDto(Guid id, string name, int rankPoints, UserLevel level, DateTime createdAt)
    {
        Id = id;
        Name = name;
        RankPoints = rankPoints;
        Level = level;
        CreatedAt = createdAt;
    }
}
