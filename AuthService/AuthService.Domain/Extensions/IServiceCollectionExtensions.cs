using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Domain.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEntityConfigurations(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(User));
        services.AddValidatorsFromAssemblyContaining<User>();
        return services;
    }
}
