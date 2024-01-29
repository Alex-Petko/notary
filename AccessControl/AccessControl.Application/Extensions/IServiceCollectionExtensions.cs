using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccessControl.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<INowGetService, NowGetService>();

        services.AddTransient<ITokenManager, TokenManager>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        return services;
    }
}