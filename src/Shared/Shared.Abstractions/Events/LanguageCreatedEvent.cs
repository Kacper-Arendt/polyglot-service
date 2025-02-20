namespace Shared.Abstractions.Events
{
    public class LanguageCreatedEvent : IEvent
    {
        public Guid LanguageId { get; }
        public Guid ProjectId { get; }
        public string Name { get; }
        public string Code { get; }
        public DateTime OccurredOn { get; }

        public LanguageCreatedEvent(Guid languageId, Guid projectId, string name, string code)
        {
            LanguageId = languageId;
            ProjectId = projectId;
            Name = name;
            Code = code;
            OccurredOn = DateTime.UtcNow;
        }

    }
}