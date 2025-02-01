using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects.Core.Database;
using Projects.Core.Repositories;
using Projects.Core.Services;
using Shared.Infrastructure.SqlServer;

namespace Projects.Core;

public static class Extensions
{
    public static IServiceCollection AddProjectsCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<ProjectsEfContext>(connectionString);

        return services;
    } 
}