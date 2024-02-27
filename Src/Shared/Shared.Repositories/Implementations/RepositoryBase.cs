using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Repositories;

[ExcludeFromCodeCoverage]
public class RepositoryBase<TEntity, TKey>
    : IRepositoryBase<TEntity, TKey> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAsNoTracking()
        => _dbSet.AsNoTracking();

    public IQueryable<TEntity> GetAsTracking()
        => _dbSet;

    public void Add(TEntity entity)
        => _dbSet.Add(entity);

    public void AddRange(params TEntity[] entities)
        => _dbSet.AddRange(entities);

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public ValueTask<TEntity?> FindAsync(TKey key)
        => _dbSet.FindAsync(key);
}