using Shared.Infrastructure;

namespace Polyglot.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        builder.Services.AddInfrastructure();

        return builder;
    }
    
    public static WebApplication UseModules(this WebApplication app) {
        app.UseInfrastructure();

        return app;
    }
}