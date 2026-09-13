using FTS.Application.Handlers.Users.Models;
using MediatR;

namespace FTS.Application.Handlers.Users.Queries;

public static class GetUser
{
    public record Query(Guid Id) : IRequest<UserDto>;
}

