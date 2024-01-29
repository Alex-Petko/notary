using DebtManager.Application;
using DebtManager.Infrastructure;
using Shared.IApplicationBuilderExtensions;

namespace DebtManager.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration)
            .AddApi(builder.Configuration);

        var app = builder.Build();

        app
            .ApplyMigration<Program, Context>()

            .UseDeveloperExceptionPage()

            .UseAuthentication()
            .UseAuthorization()

            .UseOpenApi()
            .UseSwaggerUi();

        app.MapControllers();

        app.Run();
    }
}