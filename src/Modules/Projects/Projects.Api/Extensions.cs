using Microsoft.AspNetCore.Builder;
using Projects.Core;

namespace Projects.Api;

public static class Extensions
{
    public static WebApplicationBuilder RegisterProjectsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddProjectsCore(builder.Configuration); 

        return builder;
    }

    public static WebApplication UseProjectsModule(this WebApplication app)
    {
        return app;
    }
}