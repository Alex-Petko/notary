namespace Shared.Repositories;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : class
{
    void Add(TEntity entity);

    void AddRange(params TEntity[] entities);

    ValueTask<TEntity?> FindAsync(TKey key);

    IQueryable<TEntity> GetAsNoTracking();

    IQueryable<TEntity> GetAsTracking();

    void Remove(TEntity entity);
}