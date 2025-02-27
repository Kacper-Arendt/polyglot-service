using Organizations.Core.Dtos;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;
using Organizations.Core.Repositories;
using Shared.Infrastructure.Helpers;

namespace Organizations.Core.Services;

public class OrganizationMembersService : IOrganizationMembersService
{
    private readonly IOrganizationMemberRepository _organizationMemberRepository;
    private readonly OrganizationModuleApi _organizationModuleApi;
    private readonly HttpContextHelper _httpContextHelper;

    public OrganizationMembersService(
        IOrganizationMemberRepository organizationMemberRepository, 
        OrganizationModuleApi organizationModuleApi,
        HttpContextHelper httpContextHelper)
    {
        _organizationMemberRepository = organizationMemberRepository;
        _organizationModuleApi = organizationModuleApi;
        _httpContextHelper = httpContextHelper;
    }
    
    public async Task<List<OrganizationMemberDto>> GetOrganizationMembersAsync(Guid organizationId)
    {
        var organizationMembers = await _organizationMemberRepository.GetAllAsync(organizationId);
        
        return organizationMembers.Select(organizationMember =>
            new OrganizationMemberDto(
                organizationMember.Id, 
                organizationMember.OrganizationId, 
                organizationMember.UserId, 
                (int)organizationMember.Role)
            )
            .ToList();
    }

    public async Task<OrganizationMemberDto?> GetOrganizationMemberAsync(Guid organizationId, Guid userId)
    {
        var organizationMember = await _organizationMemberRepository.GetAsync(organizationId, userId);

        return organizationMember is null
            ? null
            : new OrganizationMemberDto(
                organizationMember.Id, 
                organizationMember.OrganizationId, 
                organizationMember.UserId, 
                (int)organizationMember.Role);
    }

    public async Task AddOrganizationMemberAsync(OrganizationMemberToSetDto organizationMember)
    {
        var organizationExists = await _organizationModuleApi.OrganizationExistsAsync(organizationMember.OrganizationId);
        
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
            organizationMember.Role,
            organizationMember.Email
        );
        
        await _organizationMemberRepository.AddAsync(newOrganizationMember);
    }

    public async Task DeleteOrganizationMemberAsync(Guid organizationId, Guid userId)
    {
        var organizationExists = await _organizationModuleApi.OrganizationExistsAsync(organizationId);
        
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
}