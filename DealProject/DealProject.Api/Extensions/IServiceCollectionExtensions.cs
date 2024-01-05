using DealProject.Infrastructure;
using Shared.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Api;

[ExcludeFromCodeCoverage]
public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddDealControllers(
        this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ValueProviderFactories.Add(new ClaimValueProviderFactory());
        });

        return services;
    }

    public static IServiceCollection AddDealRepository(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddInfrastructure(connectionString);

        return services;
    }
}