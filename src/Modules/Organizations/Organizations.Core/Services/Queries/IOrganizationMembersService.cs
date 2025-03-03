using Organizations.Core.Dtos;

namespace Organizations.Core.Services.Queries;

public interface IOrganizationMembersQueryService
{
    Task<List<OrganizationMemberDto>> GetOrganizationMembersAsync(Guid organizationId);
    Task<OrganizationMemberDto?> GetOrganizationMemberAsync(Guid organizationId, Guid userId);
}