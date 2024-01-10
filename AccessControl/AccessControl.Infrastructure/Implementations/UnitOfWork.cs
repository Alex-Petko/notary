using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UnitOfWork : UnitOfWorkBase, IUnitOfWork
{
    public IUserRepository Users { get; }

    public UnitOfWork(UserContext context) : base(context)
    {
        Users = new UserRepository(context);
    }
}