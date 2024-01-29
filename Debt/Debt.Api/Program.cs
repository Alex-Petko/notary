using DebtManager.Application;
using DebtManager.Infrastructure;
using Shared.IApplicationBuilderExtensions;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Api;

[ExcludeFromCodeCoverage]
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
            .ApplyMigration<Program, DealContext>()

            .UseDeveloperExceptionPage()

            .UseAuthentication()
            .UseAuthorization()

            .UseOpenApi()
            .UseSwaggerUi();

        app.MapControllers();

        app.Run();
    }
}