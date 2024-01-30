using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Notary.Tests;

internal abstract class ApiFactoryBase<TEntryPoint, TContext> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
    where TContext : DbContext
{
    protected const string DefaultDatabase = "test_db";
    protected const string DefaultUsername = "postgres";
    protected const string DefaultPassword = "1234";

    private readonly string _connectionString;

    public ApiFactoryBase(
        string host,
        int port,
        string database = DefaultDatabase,
        string userName = DefaultUsername,
        string password = DefaultPassword)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = database,
            Username = userName,
            Password = password
        };

        _connectionString = connectionStringBuilder.ConnectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            RemoveContext(services);
            AddTestContext(services);
            SettingDatabase(services);
        });
    }


    protected virtual void AddTestData(TContext context)
    {

    }

    private void RemoveContext(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
        if (descriptor is not null)
            services.Remove(descriptor);
    }

    private void AddTestContext(IServiceCollection services)
    {
        services.AddDbContextPool<TContext>(
            opts => opts.UseNpgsql(_connectionString));
    }

    private void SettingDatabase(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        AddTestData(context);
    }
}
