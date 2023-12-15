﻿using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Repositories;

public interface IBaseRepositoryHub : IDisposable, IAsyncDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    void EnsureDatabaseCreated();
    void EnsureDatabaseDeleted();
    void Migrate();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}