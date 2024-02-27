using AccessControl.Domain;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;
using Shared.FluentValidation;

namespace AccessControl.Api;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("JwtOptions");
        services.AddOptions<JwtOptions>().Bind(section);

        services
           .AddHttpContextAccessor()
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

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        services.AddInfrastructure(connectionString);

        return services;
    }
}