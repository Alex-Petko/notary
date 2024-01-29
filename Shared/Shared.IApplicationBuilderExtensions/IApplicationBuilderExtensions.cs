using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shared.IApplicationBuilderExtensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigration<T, TContext>(
        this IApplicationBuilder app)
        where TContext : DbContext
    {
        ILogger<T> logger = null!;
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();

            using var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }
        catch (Exception e)
        {
            logger!.LogError(e, e.Message);
        }

        return app;
    }
}