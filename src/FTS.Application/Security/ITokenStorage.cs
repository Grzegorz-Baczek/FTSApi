using FTS.Application.DTO;

namespace FTS.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}
