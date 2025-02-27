namespace Organizations.Shared.Dto;

public interface IOrganizationModuleApi
{
    Task<bool> OrganizationExistsAsync(Guid id);
    Task<bool> OrganizationMemberExistsAsync(Guid organizationId, Guid userId);
    Task<bool> CanEditOrganizationAsync(Guid organizationId, Guid userId);
}