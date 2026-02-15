using FTS.Application.DTO;
using FTS.Application.Handlers.Users.Queries.GetUserById;
using FTS.Infrastructure.Exceptions.NotFoundExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Users.GetUserById;

internal sealed class GetUserQueryHandler(FTSDbContext dbContext) : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userDto = await dbContext.Users
            .Where(u => u.Id == query.Id)
            .Select(u => new UserDto(u.Id, u.Name, u.RankPoints, u.Level, u.CreatedAt))
            .SingleOrDefaultAsync(cancellationToken);
        if (userDto == null)
        {
            throw new NotFoundUserException(query.Id);
        }

        return userDto;
    }
}
