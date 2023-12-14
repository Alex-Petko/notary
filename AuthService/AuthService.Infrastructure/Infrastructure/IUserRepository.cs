using AuthService.Domain;
using Shared.Repositories;

namespace AuthService.Infrastructure;

public interface IUserRepository : IRepository<User, string>
{
}