using Organizations.Core.Dtos;

namespace Organizations.Core.Services.Commands;

public interface IOrganizationMembersCommandService
{
    Task AddOrganizationMemberAsync(OrganizationMemberToSetDto organizationMember);
    Task DeleteOrganizationMemberAsync(Guid organizationId, Guid userId);
    Task CreateOwnerAsync(Guid organizationId, Guid userId);
}