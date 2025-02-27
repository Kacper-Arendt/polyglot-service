using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Core.Database;
using Organizations.Core.Repositories;
using Organizations.Core.Services;
using Organizations.Shared.Dto;
using Shared.Infrastructure.SqlServer;

namespace Organizations.Core;

public static class Extensions
{
    public static IServiceCollection AddOrgnizationsCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrganizationMemberRepository, OrganizationMemberRepository>();
        services.AddScoped<IOrganizationMembersService, OrganizationMembersService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IOrganizationModuleApi, OrganizationModuleApi>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<OrganizationsEfContext>(connectionString);

        return services;
    } 
}