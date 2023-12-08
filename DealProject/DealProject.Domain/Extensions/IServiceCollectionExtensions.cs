using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DealProject.Domain.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEntityConfigurations(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(InitDebtDto));
        services.AddValidatorsFromAssemblyContaining<InitDebtDto>();
        return services;
    }
}
