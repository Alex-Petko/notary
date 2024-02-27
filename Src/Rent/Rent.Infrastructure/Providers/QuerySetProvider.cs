using Microsoft.EntityFrameworkCore;
using Rent.Application;

namespace Rent.Infrastructure;

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