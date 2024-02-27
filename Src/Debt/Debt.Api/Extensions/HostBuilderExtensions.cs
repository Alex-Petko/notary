using DebtManager.Domain;
using DebtManager.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace DebtManager.Api;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        {
            var indexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{hostBuilderContext.HostingEnvironment.EnvironmentName}-{DateTime.UtcNow:yyyy-MM}";
            var elasticsearchOptions = hostBuilderContext.Configuration.GetOptions<ElasticsearchOptions>();
            loggerConfiguration
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()

                .ReadFrom.Configuration(hostBuilderContext.Configuration)

                .WriteTo.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchOptions.Uri, UriKind.Absolute))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                    IndexFormat = indexFormat,
                    MinimumLogEventLevel = LogEventLevel.Verbose,
                })

                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName();
        });

        return builder;
    }
}