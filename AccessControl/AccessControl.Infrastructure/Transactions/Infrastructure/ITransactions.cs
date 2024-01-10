using AccessControl.Domain;

namespace AccessControl.Infrastructure;

public interface ITransactions
{
    Task<bool> ContainsAsync(User user);

    Task<bool> TryCreateAsync(User user);
}