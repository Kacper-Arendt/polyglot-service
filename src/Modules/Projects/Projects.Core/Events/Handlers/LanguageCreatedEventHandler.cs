using Shared.Abstractions.Events;

namespace Projects.Core.Events.Handlers
{
    public class LanguageCreatedEventHandler : IEventHandler<LanguageCreatedEvent>
    {
        public async Task HandleAsync(LanguageCreatedEvent @event)
        {
            Console.WriteLine($"Project received language created event: {@event.Name} {@event.OccurredOn}");
            await Task.CompletedTask;
        }
    }
}