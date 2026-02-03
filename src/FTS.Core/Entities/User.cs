using FTS.Core.Enum;
using FTS.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace FTS.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }

    //relacje
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<Cookbook> Cookbooks { get; set; } = new List<Cookbook>();
    public ICollection<PointsLog> PointsLogs { get; set; } = new List<PointsLog>();

    public User() { }

    public static User Create(
        string name,
        string email, 
        string passwordHash,
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
            PasswordHash = passwordHash,
            RankPoints = rankPoints,
            Level = level,
            CreatedAt = createdAt
        };
    }
}
