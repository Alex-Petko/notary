using AccessControl.Domain;
using Shared.Repositories;

namespace AccessControl.Infrastructure;

public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken, string>
{
}