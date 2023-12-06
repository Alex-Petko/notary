using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(option =>
//    {
//        option.TokenValidationParameters = new()
//        {
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//           // IssuerSigningKey = builder.Configuration["jwt:secret"]
//        };
//    });

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();
app.Run();
