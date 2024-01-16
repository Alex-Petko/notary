using DebtManager.Application;
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
            .AddInfrastructure(builder.Configuration)
            .AddApplication()
            .AddApi(builder.Configuration);

        var app = builder.Build();

        app
            .ApplyMigration<Program>()
            .UseDeveloperExceptionPage()
            .UseAuthentication()
            .UseAuthorization()
            .UseOpenApi()
            .UseSwaggerUi();

        app.MapControllers();

        app.Run();
    }
}