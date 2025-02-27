using Organizations.Core.Dtos;
using Organizations.Core.Entities;

namespace Organizations.Core.Services;

public interface IOrganizationMembersService
{
    Task<List<OrganizationMemberDto>> GetOrganizationMembersAsync(Guid organizationId);
    Task<OrganizationMemberDto?> GetOrganizationMemberAsync(Guid organizationId, Guid userId);
    Task AddOrganizationMemberAsync(OrganizationMemberToSetDto organizationMember);
    Task DeleteOrganizationMemberAsync(Guid organizationId, Guid userId);
}