using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Access;
using SGCAE.Application.AccessPoints;
using SGCAE.Application.Credentials;
using SGCAE.Application.Employees;
using SGCAE.Infrastructure.Persistence;
using SGCAE.Infrastructure.Persistence.ReadServices;
using SGCAE.Infrastructure.Persistence.Repositories;

namespace SGCAE.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        // El mismo AppDbContext sirve como Unit of Work
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        // Lado de escritura
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICredentialRepository, CredentialRepository>();
        services.AddScoped<IAccessPointRepository, AccessPointRepository>();
        services.AddScoped<IAccessLogRepository, AccessLogRepository>();

        // Lado de lectura
        services.AddScoped<IEmployeeReadService, EmployeeReadService>();
        services.AddScoped<ICredentialReadService, CredentialReadService>();
        services.AddScoped<IAccessPointReadService, AccessPointReadService>();
        services.AddScoped<IAccessLogReadService, AccessLogReadService>();

        return services;
    }
}