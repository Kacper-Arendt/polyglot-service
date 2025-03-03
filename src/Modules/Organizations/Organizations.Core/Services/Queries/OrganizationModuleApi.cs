using Organizations.Core.Entities;
using Organizations.Core.Repositories;
using Organizations.Shared;

namespace Organizations.Core.Services.Queries;

public class OrganizationModuleApi: IOrganizationModuleApi
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IOrganizationMemberRepository _organizationMemberRepository;

    public OrganizationModuleApi(IOrganizationRepository organizationRepository, IOrganizationMemberRepository organizationMemberRepository)
    {
        _organizationRepository = organizationRepository;
        _organizationMemberRepository = organizationMemberRepository;
    }
    
    public async Task<bool> ExistsAsync(Guid id)
        => await _organizationRepository.ExistsAsync(id);

    public async Task<bool> OrganizationMemberExistsAsync(Guid organizationId, Guid userId)
        => await _organizationMemberRepository.ExistsAsync(organizationId, userId);

    public async Task<bool> MemberExistsAsync(Guid organizationId, Guid userId)
    {
        var organizationMember = await _organizationMemberRepository.GetAsync(organizationId, userId);

        return organizationMember is not null;
    }

    public async Task<bool> CanEditOrganizationAsync(Guid organizationId, Guid userId)
    {
        var organizationMember = await _organizationMemberRepository.GetAsync(organizationId, userId);

        return organizationMember?.Role is OrganizationRole.Admin or OrganizationRole.Owner;
    }
}