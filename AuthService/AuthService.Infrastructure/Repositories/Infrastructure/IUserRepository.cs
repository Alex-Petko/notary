using AuthService.Domain;

namespace AuthService.Infrastructure;

public interface IUserRepository
{
    Task<bool> TryCreateUser(User user);

    Task<bool> Contains(User user);
}