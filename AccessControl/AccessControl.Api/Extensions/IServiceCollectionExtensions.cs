using AccessControl.Application;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;
using Shared.FluentValidation;

namespace AccessControl.Api;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtOptions");
        services.AddOptions<JwtOptions>().Bind(section);

        services
           .AddHttpContextAccessor()
           .Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true)
           .AddSwaggerGen(options =>
           {
               options.OperationFilter<OpenApiParameterRemover<FromClaimAttribute>>();
               options.OperationFilter<OpenApiParameterRemover<SwaggerIgnoreAttribute>>();
           })
           .AddAuthorization()
           .AddJwtAuthentication(configuration)
           .AddControllers(options =>
           {
               options.ValueProviderFactories.Add(new ClaimValueProviderFactory());
           })
           .AddAutoValidation();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddInfrastructure(connectionString);

        return services;
    }
}