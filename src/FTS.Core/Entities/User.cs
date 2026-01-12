using FTS.Core.Enum;

namespace FTS.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }

    //relacje
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<Cookbook> Cookbooks { get; set; } = new List<Cookbook>();
    public ICollection<PointsLog> PointsLogs { get; set; } = new List<PointsLog>();
}
