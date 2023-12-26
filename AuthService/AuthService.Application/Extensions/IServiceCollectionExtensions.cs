using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
