using FTS.Application.Abstractions;

namespace FTS.Infrastructure.DAL.Repositories;

internal abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected FTSDbContext DbContext { get; }

    protected BaseRepository(FTSDbContext dbContext)
        => DbContext = dbContext;

    public Task AddAsync(T entity, CancellationToken cancellationToken) 
    {
        DbContext.Set<T>().Add(entity);
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken) 
    {
        DbContext.Set<T>().Update(entity);
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken)
    { 
        DbContext.Set<T>().Remove(entity);
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
