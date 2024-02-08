using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;
using Shared.FluentValidation;
using Shared.Json;

namespace DebtManager.Api;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true)
            .AddOpenApiDocument()
            .AddAuthorization()
            .AddJwtAuthentication(configuration)
            .AddControllers(options =>
            {
                options.ValueProviderFactories.Add(new ClaimValueProviderFactory());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
            })
            .AddAutoValidation();

        return services;
    }
}