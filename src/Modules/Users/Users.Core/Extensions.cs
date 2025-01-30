using Shared.Infrastructure.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.Database;
using Users.Core.Services;

namespace Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsersCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<UsersEfContext>(connectionString);

        return services;
    }    
}