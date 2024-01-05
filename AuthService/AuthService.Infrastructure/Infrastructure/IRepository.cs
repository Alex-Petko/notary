using Shared.Repositories;

namespace AuthService.Infrastructure;

public interface IRepository : IUnitOfWorkBase
{
    IUserRepository Users { get; }
}