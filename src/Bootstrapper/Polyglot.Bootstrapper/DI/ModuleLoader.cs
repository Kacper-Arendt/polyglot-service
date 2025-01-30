using Shared.Infrastructure;
using Users.Api;

namespace Polyglot.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        builder.Services.AddInfrastructure();
        builder.RegisterUsersModule();

        return builder;
    }
    
    public static WebApplication UseModules(this WebApplication app) {
        app.UseInfrastructure();
        app.UseUsersModule();

        return app;
    }
}