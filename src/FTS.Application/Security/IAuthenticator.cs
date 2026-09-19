using FTS.Application.Handlers.Users.Models;

namespace FTS.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, IEnumerable<string> roles);
}
