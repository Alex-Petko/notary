using DebtManager.Domain;

namespace DebtManager.Application;

public interface IQueryUsersProvider
{
    Task<User?> FindAsync(string login, CancellationToken cancellationToken = default);
}
