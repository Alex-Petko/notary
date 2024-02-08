using AccessControl.Client;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;

namespace DebtManager.Infrastructure;

internal sealed class QueryUsersProvider : IQueryUsersProvider
{
    private readonly IUsersClient _usersClient;
    private readonly ICache _cache;
    private readonly IMapper _mapper;

    public QueryUsersProvider(
        IUsersClient usersClient,
        ICache cache,
        IMapper mapper)
    {
        _usersClient = usersClient;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<User?> FindAsync(
        string login,
        CancellationToken cancellationToken = default)
    {
        var cacheId = $"user-{login}";

        var result = await _cache.GetAsync(
            cacheId,
            async () =>
            {
                var result = await _usersClient.GetUserQueryAsync(login, cancellationToken);
                return _mapper.Map<User>(result);
            },
            cancellationToken);

        return result;
    }
}
