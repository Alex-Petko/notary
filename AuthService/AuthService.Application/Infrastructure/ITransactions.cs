using AuthService.Domain;

namespace AuthService.Application;
internal interface ITransactions
{
    Task<bool> ContainsAsync(User user);
    Task<bool> TryCreateAsync(User user);
}