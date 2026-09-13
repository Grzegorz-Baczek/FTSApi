using FTS.Application.Abstractions;
using FTS.Application.Handlers.Cookbooks.Models;
using FTS.Application.Handlers.Cookbooks.Queries;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Handlers.Cookbooks;

internal sealed class GetCookbookHandler(
    FTSDbContext dbContext,
    ICurrentUser currentUser) : IRequestHandler<GetCookbook.Query, CookbookDto>
{
    public async Task<CookbookDto> Handle(GetCookbook.Query query, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;

        var cookbook = await dbContext.Cookbooks
            .Where(cb => cb.Id == query.Id && cb.UserId == userId)
            .Select(cb => new CookbookDto(cb.Id, cb.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (cookbook is null)
        {
            throw new NotFoundException<Cookbook>(query.Id);
        }

        return cookbook;
    }
}
