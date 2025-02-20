using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.Events;

public class EventListener : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventListener> _logger;

    public EventListener(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<EventListener> logger)
    {
        var client = new ServiceBusClient(configuration["Bus:ConnectionString"]);
        _processor = client.CreateProcessor("default");
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var eventType = args.Message.ApplicationProperties["EventType"].ToString();
        _logger.LogInformation("Received event of type: {EventType}", eventType);

        if (eventType == typeof(LanguageCreatedEvent).AssemblyQualifiedName)
        {
            var eventData = JsonSerializer.Deserialize<LanguageCreatedEvent>(args.Message.Body.ToString());
            if (eventData != null)
            {
                using var scope = _serviceProvider.CreateScope();
                var handlers = scope.ServiceProvider.GetServices<IEventHandler<LanguageCreatedEvent>>();
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(eventData);
                }
            }
        }

        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "Error processing message");
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _processor.StopProcessingAsync(cancellationToken);
        await _processor.DisposeAsync();
    }
}
