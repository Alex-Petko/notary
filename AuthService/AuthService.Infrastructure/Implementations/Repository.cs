using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class Repository : BaseRepositoryHub, IRepository
{
    public IUserRepository Users { get; }

    public Repository(UserContext context) : base(context)
    {
        Users = new UserRepository(context);
    }
}