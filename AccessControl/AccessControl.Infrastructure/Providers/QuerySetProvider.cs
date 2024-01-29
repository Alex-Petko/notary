using AccessControl.Application;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure;

internal class QuerySetProvider<TEntity> : IQuerySetProvider<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public QuerySetProvider(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => _dbSet.FindAsync(keyValues);
}