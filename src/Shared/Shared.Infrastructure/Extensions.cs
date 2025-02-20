using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Helpers;

namespace Shared.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ErrorHandlerMiddleware>();
        services.AddSingleton<HttpContextHelper>();
        
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddHostedService<EventListener>();
        
        return services;
    }    
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        // var eventSubscriber = app.Services.GetRequiredService<EventSubscriber>();
        // eventSubscriber.StartAsync().GetAwaiter().GetResult();
        
        app.UseMiddleware<ErrorHandlerMiddleware>();

        return app;
    }
}