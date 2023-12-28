using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigration(
        this IApplicationBuilder app)
    {
        ILogger<Program> logger = null!;
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
            logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            repository.Migrate();
        }
        catch (Exception e)
        {
            logger!.LogError(e, e.Message);
        }

        return app;
    }
}