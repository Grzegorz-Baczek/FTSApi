namespace FTS.Infrastructure.DAL.Repositories;

internal abstract class BaseRepository<T> where T : class
{
    private readonly FTSDbContext _dbContext;

    protected BaseRepository(FTSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(T entity, CancellationToken cancellationToken) 
    {
        _dbContext.Set<T>().Add(entity);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken) 
    {
        _dbContext.Set<T>().Update(entity);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken)
    { 
        _dbContext.Set<T>().Remove(entity); 
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
