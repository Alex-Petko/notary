using AuthService.Domain;
using Shared.Repositories;

namespace AuthService.Infrastructure;

public interface IUserRepository : IRepositoryBase<User, string>
{
}