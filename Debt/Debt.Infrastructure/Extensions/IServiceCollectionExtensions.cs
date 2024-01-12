using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Infrastructure;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DealContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddEntityFrameworkNpgsql();

        services.AddScoped<IUnitOfWorkBase, UnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}