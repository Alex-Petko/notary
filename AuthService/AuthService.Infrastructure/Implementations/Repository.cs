using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal class Repository : IRepository
{
    private readonly UserContext _context;

    public IUserRepository Users { get; }

    public Repository(UserContext context)
    {
        _context = context;

        Users = new UserRepository(context);
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
        =>_context.Database.EnsureCreated();

    public void Migrate()
        => _context.Database.Migrate();

    public void Dispose() => _context.Dispose();
}