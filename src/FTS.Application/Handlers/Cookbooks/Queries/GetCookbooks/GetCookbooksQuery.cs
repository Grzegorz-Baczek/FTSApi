using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Queries.GetCookbooks;

public record GetCookbooksQuery : IRequest<IReadOnlyCollection<CookbookDto>>;
