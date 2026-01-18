namespace FTS.Core.Entities;

public class UserRole
{
    public int Id { get; set; }
    public Guid AssignedById { get; set; }
    public User AssignedBy { get; set; } = null!;
    public DateTime AssignedAt { get; set; }
    public bool IsActive { get; set; } = true;

    //relacje
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
