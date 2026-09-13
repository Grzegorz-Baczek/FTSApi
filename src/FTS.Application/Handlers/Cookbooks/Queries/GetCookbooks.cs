using FTS.Application.Handlers.Cookbooks.Models;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Queries;

public static class GetCookbooks
{
    public record Query : IRequest<IReadOnlyCollection<CookbookDto>>;
}
