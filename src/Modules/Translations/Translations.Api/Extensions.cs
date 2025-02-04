using Microsoft.AspNetCore.Builder;
using Translations.Core;

namespace Translations.Api;

public static class Extensions
{
    public static WebApplicationBuilder RegisterTranslationsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddTranslationsCore(builder.Configuration); 

        return builder;
    }

    public static WebApplication UseTranslationsModule(this WebApplication app)
    {
        return app;
    }
}