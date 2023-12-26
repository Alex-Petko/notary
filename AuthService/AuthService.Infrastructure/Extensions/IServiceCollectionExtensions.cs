using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UserContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddEntityFrameworkNpgsql();

        services.AddScoped<IRepository, Repository>();
        services.AddTransient<ITransactions, Transactions>();

        return services;
    }
}
