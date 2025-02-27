namespace Organizations.Core.Dtos;

public record OrganizationMemberDto(Guid Id, Guid OrganizationId, Guid UserId, int Role);