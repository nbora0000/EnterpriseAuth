using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthenticationApi.Application.Interface;
using AuthenticationApi.Application.Service;
using AuthenticationApi.Infrastructure.Repository;
using AuthenticationApi.Infrastructure.CustomLogic;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AuthenticationApi.Extension;
public static class ServiceExtentionMethod
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]
                                                            ?? throw new InvalidOperationException("JWT Key is not configured."))),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"]
            };
        });
        return services;
    }
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<SecurityJwtToken>();
        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
        services.AddScoped<ISecretService, SecretService>();
        services.AddScoped<ISecretRepository, SecretRepository>();
        return services;
    }
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                                              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                                              );
        return services;
    }
    public static IConfigurationManager AddEnvConfig(this IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsetting.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        return configuration;
    }
    public static IServiceCollection AddSwaggerGenConfig(this IServiceCollection service)
    {
        service.AddSwaggerGen(c => 
        { c.SwaggerDoc("v1", new OpenApiInfo 
            {   Title = "AuthenticationApi",
                Version = "v1" 
            });
        });
        return service;
    }
}

