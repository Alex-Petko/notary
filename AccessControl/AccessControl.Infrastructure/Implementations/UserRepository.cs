using AccessControl.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : RepositoryBase<User, string>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ContainsAsync(User user)
    {
        var userFromDb = await GetAsNoTracking().FirstOrDefaultAsync(x =>
            x.Login == user.Login &&
            x.PasswordHash == user.PasswordHash);

        return userFromDb != null;
    }
}