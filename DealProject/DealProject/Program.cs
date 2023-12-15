using DealProject;
using DealProject.Application;
using DealProject.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assebly = Assembly.GetExecutingAssembly();
        builder.Services.AddValidatorsFromAssembly(assebly);

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddRepository(connectionString);

        builder.Services.AddApplication();

        var section = builder.Configuration.GetSection("JwtOptions");
        builder.Services.AddOptions<JwtOptions>().Bind(section);

        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddAuthorization();

        JwtOptions jwtOptions = null!;
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key))
                };

                option.MapInboundClaims = false;
            });

        var app = builder.Build();

        jwtOptions = app.Services.GetService<IOptions<JwtOptions>>()!.Value;

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

        app.UseDeveloperExceptionPage();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}