using AuthService;
using AuthService.Domain.Extensions;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//Integrated Security = true

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddEntityConfigurations();
builder.Services.AddRepository(connectionString);
builder.Services.AddControllers();
builder.Services.AddOptions<JwtOptions>().Bind(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//try
//{
//    using (var scope = app.Services.CreateScope())
//    using (var repository = scope.ServiceProvider.GetService<IRepository>()!)
//    {
//        repository.EnsureDatabaseCreated();
//    }
//}
//catch (Exception e)
//{

//}

app.MapControllers();

app.Run();