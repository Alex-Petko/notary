using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Notary.Tests;

internal sealed class ContainerFactory
{
    public IContainer CreateContainer(string password)
    {
        return new ContainerBuilder()
            .WithName(Guid.NewGuid().ToString("N"))
            .WithImage("postgres:15")
            .WithHostname(Guid.NewGuid().ToString("N"))
            .WithExposedPort(5432)
            .WithPortBinding(5432, true)
            .WithEnvironment("POSTGRES_PASSWORD", password)
            .WithEnvironment("PGDATA", "/pgdata")
            .WithTmpfsMount("/pgdata")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("psql -U postgres -c \"select 1\""))
            .Build();
    }
}
