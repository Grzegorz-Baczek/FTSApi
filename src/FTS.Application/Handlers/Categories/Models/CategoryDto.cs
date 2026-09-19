using System.Linq.Expressions;
using FTS.Core.Entities;

namespace FTS.Application.Handlers.Categories.Models;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public static Expression<Func<Category, CategoryDto>> AsDto =>
        c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        };
}
