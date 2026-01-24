using FTS.Application.DTO;
using MediatR;

namespace FTS.Application.Handlers.Categories.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<IReadOnlyCollection<CategoryDto>>;
