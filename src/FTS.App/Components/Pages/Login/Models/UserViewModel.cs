using FTS.App.Components.Enum;

namespace FTS.App.Components.Pages.Login.Models;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserViewModel(string name, int rankPoints, UserLevel level, DateTime createdAt)
    {
        Name = name;
        RankPoints = rankPoints;
        Level = level;
        CreatedAt = createdAt;
    }
}
