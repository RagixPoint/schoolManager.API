using System;
using System.Reflection;
using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Interfaces.Auth;
using SchoolManager.Infrastructure.Data;
using SchoolManager.Infrastructure.Services.Auth;

namespace SchoolManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString, options => options.MigrationsAssembly("SchoolManager.Infrastructure"));
        }, ServiceLifetime.Transient);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(SchoolManager.Application.Interfaces.Auth.IJwtService)) ?? throw new InvalidOperationException()));
        


        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IPasswordService, PasswordService>();
        
        return services;
    }
}