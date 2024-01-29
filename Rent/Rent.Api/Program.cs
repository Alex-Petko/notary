using Rent.Api;
using Rent.Application;
using Rent.Infrastructure;
using Shared.IApplicationBuilderExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi();

var app = builder.Build();

app
    .ApplyMigration<Program, Context>()

    .UseDeveloperExceptionPage()

    .UseOpenApi()
    .UseSwaggerUi();

app.MapControllers();

app.Run();
