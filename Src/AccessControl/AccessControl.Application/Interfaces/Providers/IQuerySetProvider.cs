namespace AccessControl.Application;

public interface IQuerySetProvider<TEntity>
    where TEntity : class
{
    Task<TEntity?> FindAsync(params object?[]? keyValues);

    Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default);
}