using AuthService.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : RepositoryBase<User, string>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {
    }
}