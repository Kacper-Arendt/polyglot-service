namespace Organizations.Shared;

public interface IOrganizationModuleApi
{
    Task<bool> ExistsAsync(Guid id);
}