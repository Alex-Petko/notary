using AuthService.Application;
using AuthService.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Api;

[ExcludeFromCodeCoverage]
internal sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services
            .AddInfrastructure(connectionString)
            .AddApplication()
            .AddSwaggerGen();

        var section = builder.Configuration.GetSection("JwtOptions");
        builder.Services.AddOptions<JwtOptions>().Bind(section);

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        ILogger<Program> logger = null!;
        try
        {
            using var scope = app.Services.CreateScope();
            using var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
            logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            repository.Migrate();
        }
        catch (Exception e)
        {
            logger!.LogError(e, e.Message);
        }

        app.MapControllers();
        app.Run();
    }
}