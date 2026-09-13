using FTS.Application.Abstractions;
using FTS.Application.Handlers.Cookbooks.Models;
using FTS.Application.Handlers.Cookbooks.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Cookbooks;

internal sealed class GetCookbooksHandler(
    FTSDbContext dbContext,
    IUserRepository userRepository) : IRequestHandler<GetCookbooks.Query, IReadOnlyCollection<CookbookDto>>
{
    public async Task<IReadOnlyCollection<CookbookDto>> Handle(GetCookbooks.Query query, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();

        var cookbooks = await dbContext.Cookbooks
            .Where(cb => cb.UserId == userId)
            .Select(cb => new CookbookDto(cb.Id, cb.Name))
            .ToListAsync(cancellationToken);

        return cookbooks;
    }
}
