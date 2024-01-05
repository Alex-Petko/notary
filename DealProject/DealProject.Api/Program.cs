using DealProject.Application;
using DealProject.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Api;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddDealRepository(builder.Configuration)
            .AddApplication()
            .AddSwaggerGen()
            .AddDealControllers()
            .AddAuthorization()
            .AddJwtAuthentication(builder.Configuration);

        var app = builder.Build();

        app.ApplyMigration();

        app.UseDeveloperExceptionPage();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}