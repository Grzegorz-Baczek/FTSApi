using FTS.Application.Handlers.Users.Models;
using FTS.Application.Handlers.Users.Queries;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserHandler(FTSDbContext dbContext) : IRequestHandler<GetUser.Query, UserDto>
{
    public async Task<UserDto> Handle(GetUser.Query query, CancellationToken cancellationToken)
    {
        var userDto = await dbContext.Users
            .Where(u => u.Id == query.Id)
            .Select(UserDto.AsDto)
            .SingleOrDefaultAsync(cancellationToken);

        if (userDto == null)
        {
            throw new NotFoundException<User>(query.Id);
        }

        return userDto;
    }
}
