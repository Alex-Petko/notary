namespace DebtManager.Infrastructure;

internal interface ICache
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

    Task<T?> GetAsync<T>(string key, Func<Task<T?>> set, CancellationToken cancellationToken = default)
       where T : class;

    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;
}