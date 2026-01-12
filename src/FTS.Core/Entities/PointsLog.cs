using FTS.Core.Enum;

namespace FTS.Core.Entities;

public class PointsLog
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    //relacje
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public int Points { get; set; }
    public PointsReason Reason { get; set; }
}
