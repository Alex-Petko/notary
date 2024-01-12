using AccessControl.Application;
using Shared.IApplicationBuilderExtensions;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Api;

[ExcludeFromCodeCoverage]
internal sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplication()
            .AddApi(builder.Configuration);

        var app = builder.Build();

        app.ApplyMigration<Program>();

        app.UseDeveloperExceptionPage();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}