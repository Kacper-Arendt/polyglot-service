using Organizations.Core.Dtos;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;
using Organizations.Core.Repositories;
using Organizations.Shared;
using Shared.Abstractions.Events;
using Shared.Abstractions.Events.Organizations;
using Shared.Infrastructure.Helpers;

namespace Organizations.Core.Services.Commands;

public class OrganizationCommandService: IOrganizationCommandService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IOrganizationModuleApi _organizationModuleApi;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IEventPublisher _eventPublisher;
    private readonly IOrganizationMembersCommandService _organizationMembersCommandService;

    public OrganizationCommandService(
        IOrganizationRepository organizationRepository,
        HttpContextHelper httpContextHelper,
        IOrganizationModuleApi organizationModuleApi, 
        IEventPublisher eventPublisher,
        IOrganizationMembersCommandService organizationMembersCommandService)
    {
        _organizationRepository = organizationRepository;
        _httpContextHelper = httpContextHelper;
        _organizationModuleApi = organizationModuleApi;
        _eventPublisher = eventPublisher;
        _organizationMembersCommandService = organizationMembersCommandService;
    }
    
    public async Task<Guid> AddAsync(OrganizationToSetDto organization)
    {
        var newOrganization = Organization.Create(Guid.NewGuid(), organization.Name);
        
        await _organizationRepository.AddAsync(newOrganization);

        var userId = _httpContextHelper.GetCurrentUserId();
        var organizationCreated = new OrganizationCreated(userId, newOrganization.Id, organization.Name);
        await _eventPublisher.PublishAsync(organizationCreated);
        
        await _organizationMembersCommandService.CreateOwnerAsync(newOrganization.Id, userId);
        
        return newOrganization.Id;
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
        
        var organizationDeleted = new OrganizationDeleted(editorId, id);
        await _eventPublisher.PublishAsync(organizationDeleted);
    }
}