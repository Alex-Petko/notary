using AccessControl.Domain;
using Shared.Repositories;

namespace AccessControl.Infrastructure;

public interface IUserRepository : IRepositoryBase<User, string>
{
    public Task<bool> ContainsAsync(User user);
}