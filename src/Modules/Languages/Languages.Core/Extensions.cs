using Languages.Core.Database;
using Languages.Core.Repositories;
using Languages.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.SqlServer;

namespace Languages.Core;

public static class Extensions
{
    public static IServiceCollection AddLanguagesCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILanguagesRepository, LanguagesRepository>();
        services.AddScoped<ILanguagesService, LanguagesService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<LanguagesEfContext>(connectionString);

        return services;
    } 
}