using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.SqlServer;

public static class Extensions 
{
    public static IServiceCollection AddSqlServerWithEfCore<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string missing");
        }
        
        services.AddDbContext<T>(x =>
        {
            x.UseSqlServer(connectionString);
            x.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        });
        
        using var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<T>();
  
        if (context.Database.CanConnect()) context.Database.Migrate();
        
        return services;
    }
}