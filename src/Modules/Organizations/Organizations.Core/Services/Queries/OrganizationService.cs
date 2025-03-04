using Organizations.Core.Dtos;
using Organizations.Core.Repositories;
using Shared.Infrastructure.Helpers;

namespace Organizations.Core.Services.Queries;

public class OrganizationQueryService: IOrganizationQueryService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly HttpContextHelper _httpContextHelper;

    public OrganizationQueryService(IOrganizationRepository organizationRepository, HttpContextHelper httpContextHelper)
    {
        _organizationRepository = organizationRepository;
        _httpContextHelper = httpContextHelper;
    }
    
    public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        var userId = _httpContextHelper.GetCurrentUserId();
        var organizations = await _organizationRepository.GetAllAsync(userId);

        return organizations.Select(organization => new OrganizationDto(organization.Id, organization.Name));
    }

    public async Task<OrganizationDto?> GetAsync(Guid id)
    {
        var organization = await _organizationRepository.GetAsync(id);

        return organization is null ? null : new OrganizationDto(organization.Id, organization.Name);
    }
}