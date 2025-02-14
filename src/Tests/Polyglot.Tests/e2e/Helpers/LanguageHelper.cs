using System.Net.Http.Json;
using Languages.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public static class LanguageHelper
{
    public static async Task<Guid> CreateLanguageAsync(HttpClient client, Guid projectId,
        LanguageToSetDto languageToSetDto)
    {
        var response = await client.PostAsJsonAsync($"/api/projects/{projectId}/languages", languageToSetDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public static async Task<LanguageToReadDto?> GetLanguageByIdAsync(HttpClient client, Guid projectId,
        Guid languageId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/languages/{languageId}");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<LanguageToReadDto>() : null;
    }

    public static async Task<IEnumerable<LanguageToReadDto>?> GetAllLanguagesAsync(HttpClient client, Guid projectId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/languages");

        return await response.Content.ReadFromJsonAsync<IEnumerable<LanguageToReadDto>>();
    }

    public static async Task UpdateLanguageAsync(HttpClient client, Guid projectId, Guid languageId,
        LanguageToSetDto updatedLanguage)
    {
        await client.PutAsJsonAsync($"/api/projects/{projectId}/languages/{languageId}", updatedLanguage);
    }

    public static async Task DeleteLanguageAsync(HttpClient client, Guid projectId, Guid languageId)
    {
        await client.DeleteAsync($"/api/projects/{projectId}/languages/{languageId}");
    }
}