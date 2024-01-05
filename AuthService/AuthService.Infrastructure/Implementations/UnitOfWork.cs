using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UnitOfWork : UnitOfWorkBase, IRepository
{
    public IUserRepository Users { get; }

    public UnitOfWork(UserContext context) : base(context)
    {
        Users = new UserRepository(context);
    }
}