using FTS.Application.Handlers.Categories.Models;
using MediatR;

namespace FTS.Application.Handlers.Categories.Queries;

public static class GetCategories
{
    public record Query : IRequest<IReadOnlyCollection<CategoryDto>>;
}
