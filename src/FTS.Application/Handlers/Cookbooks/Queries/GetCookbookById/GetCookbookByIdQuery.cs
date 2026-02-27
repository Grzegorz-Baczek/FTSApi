using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Queries.GetCookbookById;

public record GetCookbookByIdQuery(Guid Id) : IRequest<CookbookDto>;
