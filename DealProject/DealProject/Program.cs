using DealProject;
using DealProject.Domain.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

JwtOptions jwtOptions = null!;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEntityConfigurations();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key))
        };
    });

builder.Services.AddOptions<JwtOptions>().Bind(builder.Configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();
jwtOptions = app.Services.GetService<IOptions<JwtOptions>>()!.Value;
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
