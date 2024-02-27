using AccessControl.Client;
using AutoMapper.EquivalencyExpression;
using DebtManager.Application;
using DebtManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DebtManager.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<Context>(opt =>
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>();
            opt.UseNpgsql(postgresOptions.ConnectionStrings.Default);
        });

        services.AddStackExchangeRedisCache(opt =>
        {
            var redisOptions = configuration.GetOptions<RedisOptions>();

            opt.Configuration = redisOptions.ConnectionString;
        });

        services.AddScoped<ICache, RedisCache>();

        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(config =>
        {
            config.AddCollectionMappers();
            config.AddMaps(assembly);
        });

        services.AddHttpClient();
        services.AddScoped<ICommandProvider, CommandProvider>();
        services.AddScoped<Application.IQueryProvider, QueryProvider>();
        services.AddScoped<IUsersClient, UsersClientProxy>(serviceProvider =>
        {
            var accessControlOptions = configuration.GetOptions<AccessControlOptions>();
            var client = new UsersClientProxy(accessControlOptions.ConnectionString, serviceProvider.GetRequiredService<HttpClient>());
            return client;
        });

        return services;
    }
}