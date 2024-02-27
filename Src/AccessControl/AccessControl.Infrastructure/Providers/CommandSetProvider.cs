using AccessControl.Application;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure;

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
