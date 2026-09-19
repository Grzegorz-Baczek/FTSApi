using System.Linq.Expressions;
using FTS.Core.Entities;

namespace FTS.Application.Handlers.Cookbooks.Models;

public class CookbookDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static Expression<Func<Cookbook, CookbookDto>> AsDto =>
        cb => new CookbookDto
        {
            Id = cb.Id,
            Name = cb.Name
        };
}
