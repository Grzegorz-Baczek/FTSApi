using System.Linq.Expressions;
using FTS.Core.Entities;
using FTS.Core.Enum;

namespace FTS.Application.Handlers.Users.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }

    public static Expression<Func<User, UserDto>> AsDto =>
        u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            RankPoints = u.RankPoints,
            Level = u.Level,
            CreatedAt = u.CreatedAt
        };
}
