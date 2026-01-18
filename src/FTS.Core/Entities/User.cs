using FTS.Core.Enum;
using FTS.Core.Exceptions;

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
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public User() { }

    public static User Create(
        string name,
        string email, 
        string password, 
        int rankPoints, 
        UserLevel level, 
        DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidNameException("Name cannot be empty");
        }

        if (rankPoints < 0)
        {
            throw new InvalidRankPointsException(rankPoints);
        }

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password,
            RankPoints = rankPoints,
            Level = level,
            CreatedAt = createdAt
        };
    }
}
