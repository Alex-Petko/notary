using AuthService.Application;
using AuthService.Infrastructure;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[ExcludeFromCodeCoverage]
internal sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetExecutingAssembly();
        builder.Services.AddValidatorsFromAssembly(assembly);

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddRepository(connectionString);

        builder.Services.AddApplication();
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<CreateTokenRequest>();
        });

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