using FTS.Application.Abstractions;
using FTS.Application.DTO;
using FTS.Application.Handlers.Cookbooks.Queries.GetCookbooks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Cookbooks.GetCookbooks;

internal sealed class GetCookbooksQueryHandler(
    FTSDbContext dbContext,
    IUserRepository userRepository) : IRequestHandler<GetCookbooksQuery, IReadOnlyCollection<CookbookDto>>
{
    public async Task<IReadOnlyCollection<CookbookDto>> Handle(GetCookbooksQuery query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var cookbooks = await dbContext.Cookbooks
            .Where(cb => cb.UserId == userId)
            .Select(cb => new CookbookDto(cb.Id, cb.Name))
            .ToListAsync(cancellationToken);

        return cookbooks;
    }
}
