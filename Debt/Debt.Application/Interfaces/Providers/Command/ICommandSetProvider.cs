namespace DebtManager.Application;

public interface ICommandSetProvider<TEntity>
    where TEntity : class
{
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
}