using FTS.Application.DTO;

namespace FTS.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}
