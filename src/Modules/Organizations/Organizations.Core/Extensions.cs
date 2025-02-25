using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Core.Database;
using Organizations.Core.Repositories;
using Shared.Infrastructure.SqlServer;

namespace Organizations.Core;

public static class Extensions
{
    public static IServiceCollection AddOrgnizationsCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        // services.AddScoped<IProjectService, ProjectService>();
        
        // services.AddTransient<IProjectsModuleApi, ProjectsModuleApi>();
        
        // services.AddScoped<IEventHandler<LanguageCreatedEvent>, LanguageCreatedEventHandler>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<OrganizationsEfContext>(connectionString);

        return services;
    } 
}