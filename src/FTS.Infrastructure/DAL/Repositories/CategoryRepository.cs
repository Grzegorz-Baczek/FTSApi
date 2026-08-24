using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CategoryRepository(FTSDbContext dbContext) :
    BaseRepository<Category>(dbContext), ICategoryRepository
{ }