using Microsoft.EntityFrameworkCore;
using DebtManager.Application;

namespace DebtManager.Infrastructure;

internal sealed class CommandSetProvider<TEntity> : ICommandSetProvider<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public CommandSetProvider(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }


    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => _dbSet.FindAsync(keyValues);
}
