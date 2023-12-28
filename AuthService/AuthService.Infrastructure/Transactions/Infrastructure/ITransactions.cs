using AuthService.Domain;

namespace AuthService.Infrastructure;

public interface ITransactions
{
    Task<bool> ContainsAsync(User user);

    Task<bool> TryCreateAsync(User user);
}