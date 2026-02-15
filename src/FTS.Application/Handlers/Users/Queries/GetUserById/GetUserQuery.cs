using FTS.Application.DTO;
using FTS.Core.Enum;
using MediatR;

namespace FTS.Application.Handlers.Users.Queries.GetUserById;

public class GetUserQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }
}
