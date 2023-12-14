using AuthService.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {
    }
}

