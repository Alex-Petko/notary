using AccessControl.Application;
using AccessControl.Infrastructure;
using Shared.FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Api;

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
        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddControllers()
            .AddAutoValidation();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        ILogger<Program> logger = null!;
        try
        {
            using var scope = app.Services.CreateScope();
            using var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
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