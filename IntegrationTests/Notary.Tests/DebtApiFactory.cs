
namespace Notary.Tests;

internal sealed class DebtApiFactory : ApiFactoryBase<DebtManager.Api.Program, DebtManager.Infrastructure.Context>
{
    public DebtApiFactory(
        string host,
        int port,
        string database = DefaultDatabase,
        string userName = DefaultUsername,
        string password = DefaultPassword) 
        : base(host, port, database, userName, password)
    {
    }
}
