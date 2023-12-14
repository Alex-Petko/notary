using AuthService.Application;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Extensions;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetExecutingAssembly();
        builder.Services.AddValidatorsFromAssembly(assembly);

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddRepository(connectionString);

        builder.Services.AddApplication();

        var section = builder.Configuration.GetSection("JwtOptions");
        builder.Services.AddOptions<JwtOptions>().Bind(section);

        builder.Services.AddSwaggerGen();
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

            repository.EnsureDatabaseCreated();
        }
        catch (Exception e)
        {
            logger!.LogError(e, e.Message);
        }

        app.MapControllers();
        app.Run();
    }
}