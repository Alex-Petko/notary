using AuthService.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AuthService.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddTransient<ITokenGenerator, TokenGenerator>();
        services.AddTransient<IUserCreator, UserCreator>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<ITransactions, Transactions>();

        return services;
    }
}
