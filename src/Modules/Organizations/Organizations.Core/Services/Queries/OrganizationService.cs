using Organizations.Core.Dtos;
using Organizations.Core.Repositories;

namespace Organizations.Core.Services.Queries;

public class OrganizationQueryService: IOrganizationQueryService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationQueryService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    
    public async Task<IEnumerable<OrganizationDto>> GetAllAsync(Guid userId)
    {
        var organizations = await _organizationRepository.GetAllAsync(userId);

        return organizations.Select(organization => new OrganizationDto(organization.Id, organization.Name));
    }

    public async Task<OrganizationDto?> GetAsync(Guid id)
    {
        var organization = await _organizationRepository.GetAsync(id);

        return organization is null ? null : new OrganizationDto(organization.Id, organization.Name);
    }
}