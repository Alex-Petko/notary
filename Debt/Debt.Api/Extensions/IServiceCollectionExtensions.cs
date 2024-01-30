using DebtManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;
using Shared.FluentValidation;
using Shared.Json;

namespace DebtManager.Api;

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
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
            })
            .AddAutoValidation();

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