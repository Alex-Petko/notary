using DebtManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;
using Shared.FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Api;

[ExcludeFromCodeCoverage]
public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true)
            .AddOpenApiDocument()
            .AddAuthorization()
            .AddJwtAuthentication(configuration)
            .AddControllers(options =>
            {
                options.ValueProviderFactories.Add(new ClaimValueProviderFactory());
            })
            .AddAutoValidation();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddInfrastructure(connectionString);

        return services;
    }
}