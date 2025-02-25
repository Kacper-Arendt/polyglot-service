using Organizations.Core.Dtos;

namespace Organizations.Core.Services;

public class OrganizationService: IOrganizationService
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(OrganizationToSetDto organization)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrganizationDto>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<OrganizationDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(OrganizationToSetDto organization)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}