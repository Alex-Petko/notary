using AutoMapper;

namespace AuthService.Infrastructure;

internal class Repository : IRepository
{
    private readonly UserContext _context;

    public IUserRepository Users { get; }

    public Repository(UserContext context)
    {
        _context = context;

        Users = new UserRepository(context);
    }

    public void EnsureDatabaseCreated()
    {
        _context.Database.EnsureCreated();
    }

    public void Dispose() => _context.Dispose();
}