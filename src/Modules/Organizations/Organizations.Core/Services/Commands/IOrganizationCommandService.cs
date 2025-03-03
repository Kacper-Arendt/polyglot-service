using Organizations.Core.Dtos;

namespace Organizations.Core.Services.Commands;

public interface IOrganizationCommandService
{
    Task AddAsync(OrganizationToSetDto organization);
    Task UpdateAsync(Guid id, OrganizationToSetDto organization);
    Task DeleteAsync(Guid id);
}