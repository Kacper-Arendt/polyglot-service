using Microsoft.AspNetCore.Builder;
using Organizations.Core;

namespace Organizations.Api;

public static class Extensions
{
    public static WebApplicationBuilder RegisterOrganizationModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddOrgnizationsCore(builder.Configuration); 

        return builder;
    }

    public static WebApplication UseOrganizationsModule(this WebApplication app)
    {
        return app;
    }
}