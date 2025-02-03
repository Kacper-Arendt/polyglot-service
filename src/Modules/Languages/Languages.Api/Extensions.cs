using Languages.Core;
using Microsoft.AspNetCore.Builder;

namespace Languages.Api;

public static class Extensions
{
    public static WebApplicationBuilder RegisterLanguagesModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddLanguagesCore(builder.Configuration); 

        return builder;
    }

    public static WebApplication UseProjectsModule(this WebApplication app)
    {
        return app;
    }
}