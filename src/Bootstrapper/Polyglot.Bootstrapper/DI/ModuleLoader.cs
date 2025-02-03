using Projects.Api;
using Projects.Api.Controllers;
using Shared.Infrastructure;
using Users.Api;
using Languages.Api;
using Languages.Api.Controllers;

namespace Polyglot.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        builder.Services.AddInfrastructure();
        builder.RegisterUsersModule();
        builder.RegisterProjectsModule();
        builder.RegisterLanguagesModule();
        
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(ProjectsController).Assembly)
            .AddApplicationPart(typeof(LanguagesController).Assembly);

        return builder;
    }
    
    public static WebApplication UseModules(this WebApplication app) {
        app.UseInfrastructure();
        app.UseUsersModule();
        app.UseProjectsModule();
        app.UseLanguagesModule();

        return app;
    }
}