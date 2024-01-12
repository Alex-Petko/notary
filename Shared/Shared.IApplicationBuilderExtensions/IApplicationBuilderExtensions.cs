using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Shared.Repositories;

namespace Shared.IApplicationBuilderExtensions;

[ExcludeFromCodeCoverage]
public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigration<T>(
        this IApplicationBuilder app)
    {
        ILogger<T> logger = null!;
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();

            using var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWorkBase>();
            repository.Migrate();
        }
        catch (Exception e)
        {
            logger!.LogError(e, e.Message);
        }

        return app;
    }
}