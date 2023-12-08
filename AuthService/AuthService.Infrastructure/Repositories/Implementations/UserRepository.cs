using AuthService.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure;

internal class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly UserContext _context;

    public UserRepository(UserContext dbContext)
    {
        _context = dbContext;
        _users = _context.Set<User>();
    }

    public async Task<bool> Contains(User user)
    {
        var userFromDb = await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == user.Login && x.PasswordHash == user.PasswordHash);

        return userFromDb != null;
    }

    public async Task<bool> TryCreateUser(User user)
    {
        var result = false;

        using var transaction = _context.Database.BeginTransaction();

        var dbUser = await _users.FindAsync(user.Login);
        if (dbUser == null)
        {
            _users.Add(user);
            result = true;
            await _context.SaveChangesAsync();
        }

        transaction.Commit();

        return result;
    }
}

