using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Core.Database;
using Organizations.Core.Repositories;
using Organizations.Core.Services;
using Organizations.Core.Services.Commands;
using Organizations.Core.Services.Queries;
using Organizations.Shared;
using Shared.Infrastructure.SqlServer;

namespace Organizations.Core;

public static class Extensions
{
    public static IServiceCollection AddOrganizationsCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrganizationMemberRepository, OrganizationMemberRepository>();
        
        services.AddScoped<IOrganizationMembersQueryService, OrganizationMembersQueryService>();
        services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
        
        services.AddScoped<IOrganizationMembersCommandService, OrganizationMembersCommandService>();
        services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
        
        services.AddScoped<IOrganizationModuleApi, OrganizationModuleApi>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<OrganizationsEfContext>(connectionString);

        return services;
    } 
}