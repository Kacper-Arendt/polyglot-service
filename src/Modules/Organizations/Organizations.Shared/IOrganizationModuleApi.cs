namespace Organizations.Shared;

public interface IOrganizationModuleApi
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> MemberExistsAsync(Guid organizationId, Guid userId);
    Task<bool> CanEditOrganizationAsync(Guid organizationId, Guid userId);
}