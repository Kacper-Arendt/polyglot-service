using System.Net.Http.Json;
using Projects.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public static class ProjectHelper
{
    public static async Task<Guid> CreateProjectAsync(HttpClient client, ProjectToCreateDto projectToSetDto)
    {
        var response = await client.PostAsJsonAsync("/api/projects", projectToSetDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public static async Task<ProjectToReadDto?> GetProjectByIdAsync(HttpClient client, Guid projectId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}");
        return await response.Content.ReadFromJsonAsync<ProjectToReadDto>();
    }

    public static async Task<IEnumerable<ProjectToReadDto>> GetAllProjectsAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/projects");
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProjectToReadDto>>();
    }

    public static async Task UpdateProjectAsync(HttpClient client, Guid projectId, ProjectToUpdateDto updatedProject)
    {
        await client.PutAsJsonAsync($"/api/projects/{projectId}", updatedProject);
    }

    public static async Task DeleteProjectAsync(HttpClient client, Guid projectId)
    {
        await client.DeleteAsync($"/api/projects/{projectId}");
    }
}