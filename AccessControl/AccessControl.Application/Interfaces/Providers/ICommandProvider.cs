using AccessControl.Domain;

namespace AccessControl.Application;

public interface ICommandProvider
{
    ICommandSetProvider<User> Users { get; }

    Task UpdateRefreshToken(
         string login,
         string newRefreshTokenString,
         DateTime timestamp,
         CancellationToken cancellationToken = default);

    Task<bool> TryCreateUserAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
