using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tasks.WebApi.Context;
using Tasks.WebApi.Entities;
using Tasks.WebApi.Repositories;
using Tasks.WebApi.Servicies;

namespace Tasks.WebApi.Extantions;

public static class ModuleExtantions
{
    public static WebApplicationBuilder AddIdentityService(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 1;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<TaskContext>()
        .AddDefaultTokenProviders();

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

        builder.Services.AddAuthentication(options =>
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
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        return builder;
    }

    public static WebApplicationBuilder AddCustomServicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRepository<TaskEntity>, TaskRepository>();
        builder.Services.AddScoped<TaskService>();
        builder.Services.AddScoped<AuthService>();

        return builder;
    }
}
