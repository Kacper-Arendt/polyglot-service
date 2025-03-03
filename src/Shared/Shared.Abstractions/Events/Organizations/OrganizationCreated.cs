namespace Shared.Abstractions.Events.Organizations;

public record OrganizationCreated(Guid CreatedBy, Guid OrganizationId, string Name): IEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
};
