using DebtManager.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DebtManager.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        string connectionString, 
        string redisConnectionString)
    {
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = redisConnectionString;
        });

        services.AddScoped<ICommandProvider, CommandProvider>();
        services.AddScoped<Application.IQueryProvider, QueryProvider>();

        return services;
    }
}