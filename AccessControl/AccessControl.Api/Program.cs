using AccessControl.Application;
using AccessControl.Infrastructure;
using Shared.IApplicationBuilderExtensions;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Api;

[ExcludeFromCodeCoverage]
internal sealed class Program
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
            .ApplyMigration<Program, UserContext>()

            .UseDeveloperExceptionPage()

            .UseAuthentication()
            .UseAuthorization()

            .UseOpenApi()
            .UseSwaggerUi();

        app.MapControllers();

        app.Run();
    }
}