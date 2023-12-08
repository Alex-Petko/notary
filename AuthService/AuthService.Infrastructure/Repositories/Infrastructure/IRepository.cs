namespace AuthService.Infrastructure;

public interface IRepository : IDisposable
{
    IUserRepository Users { get; }

    void EnsureDatabaseCreated();
}
