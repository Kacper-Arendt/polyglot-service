using Organizations.Core.Entities;

namespace Organizations.Core.Repositories;

public interface IOrganizationRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Organization? organization);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<IEnumerable<Organization>> GetAllAsync(Guid userId);
    Task<Organization?> GetAsync(Guid id);
    Task UpdateAsync(Organization organization);
    Task DeleteAsync(Guid id);
}