using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Infrastructure.Common;
using ManagementSystem.Infrastructure.Persistence.Repositories;

namespace ManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));


        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());


        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}