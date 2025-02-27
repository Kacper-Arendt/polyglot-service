using Organizations.Core.Dtos;
using Organizations.Core.Entities;

namespace Organizations.Core.Services;

public interface IOrganizationService
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(OrganizationToSetDto organization);
    Task<IEnumerable<OrganizationDto>> GetAllAsync();
    Task<IEnumerable<OrganizationDto>> GetAllAsync(Guid userId);
    Task<OrganizationDto?> GetAsync(Guid id);
    Task UpdateAsync(Guid id, OrganizationToSetDto organization);
    Task DeleteAsync(Guid id);
}