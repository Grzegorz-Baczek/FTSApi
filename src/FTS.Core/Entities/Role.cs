namespace FTS.Core.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    //Relacje
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
