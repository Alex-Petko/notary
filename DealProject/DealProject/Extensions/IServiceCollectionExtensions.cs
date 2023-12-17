using DealProject.Attributes;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DealProject.Infrastructure;

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
        services.AddRepository(connectionString);

        return services;
    }

    public static IServiceCollection AddValidators(
        this IServiceCollection services)
    {
        var assebly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assebly);

        return services;
    }
}