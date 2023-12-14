using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Infrastructure;

public interface IRepository : IDisposable
{
    IUserRepository Users { get; }

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    void EnsureDatabaseCreated();

    void Migrate();
}
