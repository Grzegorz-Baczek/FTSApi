using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CookbookRepository(FTSDbContext dbContext) :
    BaseRepository<Cookbook>(dbContext), ICookbookRepository
{ }
