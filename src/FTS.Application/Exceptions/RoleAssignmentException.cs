using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

public sealed class RoleAssignmentException : CustomException
{
    public string RoleName { get; }

    public RoleAssignmentException(string roleName, string details) : base($"Failed to assign role '{roleName}'. Details: {details}")
    {
        RoleName = roleName;
    }
}
