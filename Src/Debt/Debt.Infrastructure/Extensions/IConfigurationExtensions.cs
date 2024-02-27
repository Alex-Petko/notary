using DebtManager.Domain;
using Microsoft.Extensions.Configuration;

namespace DebtManager.Infrastructure;

public static class IConfigurationExtensions
{
    public static T GetOptions<T>(this IConfiguration configuration) 
        => configuration
            .GetRequiredSection(typeof(T).Name)
            .Get<T>();
}
