namespace Rent.Application;

public interface IQuerySetProvider<TEntity>
    where TEntity : class
{
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
}