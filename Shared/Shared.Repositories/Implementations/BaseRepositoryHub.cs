using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Repositories;

public abstract class BaseRepositoryHub : IBaseRepositoryHub
{
    private readonly DbContext _context;

    public BaseRepositoryHub(DbContext context)
    {
        _context = context;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _context.Database.BeginTransactionAsync(cancellationToken);

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        => _context.Database.CommitTransactionAsync(cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(true, cancellationToken);

    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

    public void EnsureDatabaseCreated()
        => _context.Database.EnsureCreated();

    public void EnsureDatabaseDeleted()
        => _context.Database.EnsureDeleted();

    public void Migrate()
        => _context.Database.Migrate();

    public void Dispose() 
        => _context.Dispose();
     
    public ValueTask DisposeAsync() 
        => _context.DisposeAsync();
}
