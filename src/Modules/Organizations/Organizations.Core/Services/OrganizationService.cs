using Organizations.Core.Dtos;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;
using Organizations.Core.Repositories;
using Shared.Infrastructure.Helpers;

namespace Organizations.Core.Services;

public class OrganizationService: IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly OrganizationModuleApi _organizationModuleApi;
    private readonly HttpContextHelper _httpContextHelper;

    public OrganizationService(
        IOrganizationRepository organizationRepository,
        HttpContextHelper httpContextHelper,
        OrganizationModuleApi organizationModuleApi
        )
    {
        _organizationRepository = organizationRepository;
        _httpContextHelper = httpContextHelper;
        _organizationModuleApi = organizationModuleApi;
    }
    
    public async Task<bool> ExistsAsync(Guid id)
        => await _organizationRepository.ExistsAsync(id);

    public async Task AddAsync(OrganizationToSetDto organization)
    {
        var newOrganization = Organization.Create(Guid.NewGuid(), organization.Name);
        
        await _organizationRepository.AddAsync(newOrganization);
    }

    public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        var organizations = await _organizationRepository.GetAllAsync();

        return organizations.Select(organization => new OrganizationDto(organization.Id, organization.Name));
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

    public async Task UpdateAsync(Guid id, OrganizationToSetDto organization)
    {
        var organizationToUpdate = await _organizationRepository.GetAsync(id);
        
        if (organizationToUpdate is null)
        {
            throw new OrganizationNotFoundException(id);
        }
        
        var editorId = _httpContextHelper.GetCurrentUserId();
        
        var canEdit = await _organizationModuleApi.CanEditOrganizationAsync(id, editorId);
        
        if (!canEdit)
        {
            throw new UnauthorizedOrganizationOperationException(editorId);
        }
        
        organizationToUpdate.Update(organization.Name);
        await _organizationRepository.UpdateAsync(organizationToUpdate);
    }

    public async Task DeleteAsync(Guid id)
    {
        var organization = await _organizationRepository.GetAsync(id);
        
        if (organization is null) throw new OrganizationNotFoundException(id);
        
        var editorId = _httpContextHelper.GetCurrentUserId();
        
        var canEdit = await _organizationModuleApi.CanEditOrganizationAsync(id, editorId);
        
        if (!canEdit) throw new UnauthorizedOrganizationOperationException(editorId);
        
        await _organizationRepository.DeleteAsync(id);
    }
}