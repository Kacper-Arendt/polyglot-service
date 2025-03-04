using Organizations.Core.Dtos;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;
using Organizations.Core.Repositories;
using Organizations.Shared;
using Shared.Infrastructure.Helpers;

namespace Organizations.Core.Services.Commands;

public class OrganizationMembersCommandService : IOrganizationMembersCommandService
{
    private readonly IOrganizationMemberRepository _organizationMemberRepository;
    private readonly IOrganizationModuleApi _organizationModuleApi;
    private readonly HttpContextHelper _httpContextHelper;

    public OrganizationMembersCommandService(
        IOrganizationMemberRepository organizationMemberRepository, 
        IOrganizationModuleApi organizationModuleApi,
        HttpContextHelper httpContextHelper)
    {
        _organizationMemberRepository = organizationMemberRepository;
        _organizationModuleApi = organizationModuleApi;
        _httpContextHelper = httpContextHelper;
    }
    
    public async Task AddOrganizationMemberAsync(OrganizationMemberToSetDto organizationMember)
    {
        var organizationExists = await _organizationModuleApi.ExistsAsync(organizationMember.OrganizationId);
        
        if (!organizationExists)
        {
            throw new OrganizationNotFoundException(organizationMember.OrganizationId);
        }

        var editorId = _httpContextHelper.GetCurrentUserId();
        
        var canEdit = await _organizationModuleApi.CanEditOrganizationAsync(organizationMember.OrganizationId, editorId);
        
        if (!canEdit)
        {
            throw new UnauthorizedOrganizationOperationException(editorId);
        }
        
        var organizationMemberExists = await _organizationMemberRepository.ExistsAsync(organizationMember.OrganizationId, organizationMember.UserId);
        
        if (organizationMemberExists)
        {
            throw new OrganizationMemberExistsException(organizationMember.UserId);
        }
        
        var newOrganizationMember = OrganizationMember.CreateMember(
            organizationMember.OrganizationId, 
            organizationMember.UserId, 
            organizationMember.Role
        );
        
        await _organizationMemberRepository.AddAsync(newOrganizationMember);
    }

    public async Task DeleteOrganizationMemberAsync(Guid organizationId, Guid userId)
    {
        var organizationExists = await _organizationModuleApi.ExistsAsync(organizationId);
        
        if (!organizationExists)
        {
            throw new OrganizationNotFoundException(organizationId);
        }

        var editorId = _httpContextHelper.GetCurrentUserId();
        
        var canEdit = await _organizationModuleApi.CanEditOrganizationAsync(organizationId, editorId);
        
        if (!canEdit)
        {
            throw new UnauthorizedOrganizationOperationException(editorId);
        }
        
        var organizationMember = await _organizationMemberRepository.GetAsync(organizationId, userId);
        
        if (organizationMember is null)
        {
            throw new OrganizationMemberNotFoundException(userId);
        }

        if (organizationMember.Role == OrganizationRole.Owner)
        {
            throw new OrganizationMemberIsOwnerException(userId);
        }
        
        await _organizationMemberRepository.DeleteAsync(userId);
    }

    public async Task CreateOwnerAsync(Guid organizationId, Guid userId)
    {
        var organizationMemberExists = await _organizationMemberRepository.ExistsAsync(organizationId, userId);
        
        if (organizationMemberExists)
        {
            throw new OrganizationMemberExistsException(userId);
        }
        
        var newOrganizationMember = OrganizationMember.CreateOwner(organizationId, userId);
        
        await _organizationMemberRepository.AddAsync(newOrganizationMember);   
    }
}