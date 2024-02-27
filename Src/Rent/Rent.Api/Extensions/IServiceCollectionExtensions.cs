using Microsoft.AspNetCore.Mvc;
using Rent.Infrastructure;
using Shared.FluentValidation;

namespace Rent.Api;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services)
    {

        services
           .AddHttpContextAccessor()
           .Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true)
           .AddOpenApiDocument()
           .AddControllers()
           .AddAutoValidation();

        services.AddSingleton<Application.IWebHostEnvironment, WebHostEnvironmentProxy>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        services.AddInfrastructure(connectionString);

        return services;
    }
}