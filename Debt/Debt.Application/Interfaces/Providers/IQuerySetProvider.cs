using System.Threading;

namespace DebtManager.Application;

public interface IQuerySetProvider<TEntity>
    where TEntity : class
{
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);

    Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default);
}