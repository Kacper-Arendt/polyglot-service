using Projects.Api;
using Projects.Api.Controllers;
using Shared.Infrastructure;
using Users.Api;
using Languages.Api;
using Languages.Api.Controllers;
using Organizations.Api;
using Translations.Api;
using Translations.Api.Controllers;

namespace Polyglot.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        builder.Services.AddInfrastructure();
        builder.RegisterUsersModule();
        builder.RegisterOrganizationModule();
        builder.RegisterProjectsModule();
        builder.RegisterLanguagesModule();
        builder.RegisterTranslationsModule();
        
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(ProjectsController).Assembly)
            .AddApplicationPart(typeof(LocalizedTextController).Assembly)
            .AddApplicationPart(typeof(TranslationKeyController).Assembly)
            .AddApplicationPart(typeof(LanguagesController).Assembly);

        return builder;
    }
    
    public static WebApplication UseModules(this WebApplication app) {
        app.UseInfrastructure();
        app.UseUsersModule();
        app.UseOrganizationsModule();
        app.UseProjectsModule();
        app.UseLanguagesModule();
        app.UseTranslationsModule();
        
        return app;
    }
}