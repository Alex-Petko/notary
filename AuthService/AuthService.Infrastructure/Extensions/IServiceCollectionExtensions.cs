using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<UserContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddEntityFrameworkSqlServer();

        services.AddScoped<IRepository, Repository>();

        return services;
    }
}
