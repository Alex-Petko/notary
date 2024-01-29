using AccessControl.Application;
using AccessControl.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using static Global.Constraints;

namespace AccessControl.Infrastructure;



internal class CommandProvider : ICommandProvider
{
    private readonly Context _context;

    public ICommandSetProvider<User> Users { get; }

    public CommandProvider(Context context)
    {
        _context = context;
        Users = new CommandSetProvider<User>(context.Users);
    }

    public async Task CreateRT(
        string login, 
        string newRefreshTokenString,
        DateTime timestamp,
        CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync(login);
        if (user is null)
            return;

        var oldRtEntity = await _context.RefreshTokens.FindAsync(
            new object?[] { login },
            cancellationToken: cancellationToken);

        if (oldRtEntity is null)
        {
            var newRtEntity = new RefreshToken
            {
                Login = login,
                Data = newRefreshTokenString,
                Timestamp = timestamp
            };

            _context.RefreshTokens.Add(newRtEntity);
        }
        else 
        {
            oldRtEntity.Data = newRefreshTokenString;
            oldRtEntity.Timestamp = timestamp;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshRTResult> TryRefreshRT(
        string login,
        string newRefreshTokenString,
        string oldRefreshTokenString,
        DateTime timestamp,
        CancellationToken cancellationToken = default)
    {
        var oldRtEntity = await _context.RefreshTokens.FindAsync(
            new object?[] { login }, 
            cancellationToken: cancellationToken);

        if (oldRtEntity is null)
            return RefreshRTResult.TokenNotFound;

        if (oldRtEntity.Data != oldRefreshTokenString)
            return RefreshRTResult.TokenInvalid;

        oldRtEntity.Data = newRefreshTokenString;
        oldRtEntity.Timestamp = timestamp;

        await _context.SaveChangesAsync(cancellationToken);

        return RefreshRTResult.Ok;
    }

    public async Task<bool> TryCreateUserAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var dbUser = await _context.Users.FindAsync(
            new object?[] { user.Login }, 
            cancellationToken: cancellationToken);

        if (dbUser is not null)
            return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
