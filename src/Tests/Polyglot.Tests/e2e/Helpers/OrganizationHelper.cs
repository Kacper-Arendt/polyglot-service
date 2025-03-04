using System.Net.Http.Json;
using Organizations.Core.Dtos;
using Projects.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public class OrganizationHelper
{
    public static async Task<Guid> CreateOrganizationAsync(HttpClient client, OrganizationToSetDto organizationToSetDto)
    {
        var response = await client.PostAsJsonAsync("/api/organizations", organizationToSetDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }
    
    public static async Task<OrganizationDto?> GetOrganizationByIdAsync(HttpClient client, Guid organizationId)
    {
        var response = await client.GetAsync($"/api/organizations/{organizationId}");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<OrganizationDto>() : null;
    }
    
    public static async Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/organizations");
        return await response.Content.ReadFromJsonAsync<IEnumerable<OrganizationDto>>();
    }
    
    public static async Task UpdateOrganizationAsync(HttpClient client, Guid organizationId, OrganizationToSetDto updatedOrganization)
    {
        await client.PutAsJsonAsync($"/api/organizations/{organizationId}", updatedOrganization);
    }
    
    public static async Task DeleteOrganizationAsync(HttpClient client, Guid organizationId)
    {
        await client.DeleteAsync($"/api/organizations/{organizationId}");
    }
}