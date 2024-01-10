using DebtManager.Infrastructure;
using Shared.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Api;

[ExcludeFromCodeCoverage]
public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ValueProviderFactories.Add(new ClaimValueProviderFactory());
        });

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