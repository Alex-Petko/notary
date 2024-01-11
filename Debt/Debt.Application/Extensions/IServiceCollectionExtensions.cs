using AutoMapper.EquivalencyExpression;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DebtManager.Application;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(config =>
        {
            config.AddCollectionMappers();
            config.AddMaps(assembly);
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}