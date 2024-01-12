using AccessControl.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class RefreshTokenRepository : RepositoryBase<RefreshToken, string>, IRefreshTokenRepository
{
    public RefreshTokenRepository(UserContext dbContext) : base(dbContext)
    {
    }
}