namespace DebtManager.Application;

public interface IQuerySetProvider<TEntity>
{
    Task<TEntity?> FindAsync(
        object?[] keyValues,
        CancellationToken cancellationToken = default);

    Task<TEntity?> FindAsync(
        object keyValue,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default);
}