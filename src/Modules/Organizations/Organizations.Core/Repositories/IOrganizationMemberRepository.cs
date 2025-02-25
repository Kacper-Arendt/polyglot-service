using Organizations.Core.Entities;

namespace Organizations.Core.Repositories;

public interface IOrganizationMemberRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(OrganizationMember organizationMember);
    Task<IEnumerable<OrganizationMember>> GetAllAsync(Guid organizationId);
    Task<OrganizationMember?> GetAsync(Guid id);
    Task UpdateAsync(OrganizationMember organizationMember);
    Task DeleteAsync(Guid id);
}