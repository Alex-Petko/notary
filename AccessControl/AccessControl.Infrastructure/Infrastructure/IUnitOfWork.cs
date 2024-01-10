using Shared.Repositories;

namespace AccessControl.Infrastructure;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IUserRepository Users { get; }
}