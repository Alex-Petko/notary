using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rent.Application;
using System.Diagnostics.CodeAnalysis;

namespace Rent.Infrastructure;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ICommandProvider, CommandProvider>();
        services.AddScoped<Application.IQueryProvider, QueryProvider>();

        return services;
    }
}