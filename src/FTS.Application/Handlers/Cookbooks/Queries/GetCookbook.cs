using FTS.Application.Handlers.Cookbooks.Models;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Queries;

public static class GetCookbook
{
    public record Query(Guid Id) : IRequest<CookbookDto>;
}
