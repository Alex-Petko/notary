using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DealProject.Application;

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

        services.AddTransient<IDebtService, DebtService>();

        return services;
    }
}
