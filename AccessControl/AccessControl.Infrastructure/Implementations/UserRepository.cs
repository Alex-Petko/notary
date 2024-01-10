using AccessControl.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : RepositoryBase<User, string>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {
    }
}