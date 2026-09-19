using FTS.Application.Abstractions;
using FTS.Application.Handlers.Cookbooks.Models;
using FTS.Application.Handlers.Cookbooks.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Cookbooks;

internal sealed class GetCookbooksHandler(
    FTSDbContext dbContext,
    ICurrentUser currentUser) : IRequestHandler<GetCookbooks.Query, IReadOnlyCollection<CookbookDto>>
{
    public async Task<IReadOnlyCollection<CookbookDto>> Handle(GetCookbooks.Query query, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;

        var cookbooks = await dbContext.Cookbooks
            .Where(cb => cb.UserId == userId)
            .Select(CookbookDto.AsDto)
            .ToListAsync(cancellationToken);

        return cookbooks;
    }
}
