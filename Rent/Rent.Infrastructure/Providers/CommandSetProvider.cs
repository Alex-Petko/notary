using Microsoft.EntityFrameworkCore;
using Rent.Application;

namespace Rent.Infrastructure;

internal class CommandSetProvider<TEntity> : ICommandSetProvider<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public CommandSetProvider(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    public void Add(TEntity entity) => _dbSet.Add(entity);
}
