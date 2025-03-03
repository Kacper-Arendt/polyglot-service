namespace Shared.Abstractions.Events.Organizations;

public record OrganizationDeleted(Guid DeletedBy, Guid OrganizationId) : IEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
};
