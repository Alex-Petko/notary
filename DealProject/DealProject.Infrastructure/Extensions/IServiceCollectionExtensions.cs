using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DealContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddEntityFrameworkNpgsql();

        services.AddScoped<IRepository, Repository>();

        return services;
    }
}
