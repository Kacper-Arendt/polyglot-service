using Organizations.Core.Dtos;

namespace Organizations.Core.Services.Queries;

public interface IOrganizationQueryService
{
    Task<IEnumerable<OrganizationDto>> GetAllAsync(Guid userId);
    Task<OrganizationDto?> GetAsync(Guid id);
}