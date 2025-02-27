using Organizations.Core.Entities;

namespace Organizations.Core.Repositories;

public interface IOrganizationMemberRepository
{
    Task<bool> ExistsAsync(Guid organizationId, Guid userId);
    Task AddAsync(OrganizationMember organizationMember);
    Task<IEnumerable<OrganizationMember>> GetAllAsync(Guid organizationId);
    Task<OrganizationMember?> GetAsync(Guid organizationId, Guid userId);
    Task UpdateAsync(OrganizationMember organizationMember);
    Task DeleteAsync(Guid id);
}