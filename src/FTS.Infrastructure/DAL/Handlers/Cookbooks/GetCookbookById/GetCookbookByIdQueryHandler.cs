using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Application.Handlers.Cookbooks.Queries.GetCookbookById;
using FTS.Infrastructure.Exceptions.NotFoundExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Cookbooks.GetCookbookById;

internal sealed class GetCookbookByIdQueryHandler(
    FTSDbContext dbContext,
    IUserRepository userRepository) : IRequestHandler<GetCookbookByIdQuery, CookbookDto>
{
    public async Task<CookbookDto> Handle(GetCookbookByIdQuery query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var cookbook = await dbContext.Cookbooks
            .Where(cb => cb.Id == query.Id && cb.UserId == userId)
            .Select(cb => new CookbookDto(cb.Id, cb.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (cookbook is null)
        {
            throw new NotFoundCookbookException(query.Id);
        }

        return cookbook;
    }
}
