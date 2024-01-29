using AccessControl.Application;
using AccessControl.Domain;

namespace AccessControl.Infrastructure;

internal class CommandProvider : ICommandProvider
{
    private readonly UserContext _context;

    public ICommandSetProvider<User> Users { get; }

    public CommandProvider(UserContext context)
    {
        _context = context;
        Users = new CommandSetProvider<User>(context.Users);
    }

    public async Task UpdateRefreshToken(
        string login,
        string newRefreshTokenString,
        DateTime timestamp,
        CancellationToken cancellationToken = default)
    {
        var oldRtEntity = await _context.RefreshTokens.FindAsync(login);

        if (oldRtEntity is not null)
        {
            oldRtEntity.Data = newRefreshTokenString;
            oldRtEntity.Timestamp = timestamp;
        }
        else
        {
            var newRtEntity = new RefreshToken
            {
                Login = login,
                Data = newRefreshTokenString,
                Timestamp = timestamp
            };
            _context.RefreshTokens.Add(newRtEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> TryCreateUserAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var dbUser = await _context.Users.FindAsync(user.Login);
        if (dbUser is not null)
            return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
