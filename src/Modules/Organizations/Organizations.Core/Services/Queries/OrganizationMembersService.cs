using Organizations.Core.Dtos;
using Organizations.Core.Repositories;

namespace Organizations.Core.Services.Queries;

public class OrganizationMembersQueryService : IOrganizationMembersQueryService
{
    private readonly IOrganizationMemberRepository _organizationMemberRepository;

    public OrganizationMembersQueryService(IOrganizationMemberRepository organizationMemberRepository)
    {
        _organizationMemberRepository = organizationMemberRepository;
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
}