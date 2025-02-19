using Shared.Abstractions.Events;

namespace Translations.Core.Events.Handlers
{
    public class LanguageCreatedEventHandler : IEventHandler<LanguageCreatedEvent>
    {
        public async Task HandleAsync(LanguageCreatedEvent @event)
        {
            Console.WriteLine($"Translations received language created event: {@event.Name} {@event.OccurredOn}");
            await Task.CompletedTask;
        }
    }
}