using System.Net.Http.Json;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public static class LocalizedTextHelper
{
    public static async Task<Guid> CreateLocalizedTextAsync(HttpClient client, Guid projectId, Guid translationKeyId, LocalizedTextToSet localizedTextToSetDto)
    {
        var response = await client.PostAsJsonAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}/texts", localizedTextToSetDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public static async Task<LocalizedTextToRead?> GetLocalizedTextByIdAsync(HttpClient client, Guid projectId, Guid translationKeyId, Guid localizedTextId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}/texts/{localizedTextId}");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<LocalizedTextToRead>() : null;
    }

    public static async Task<IEnumerable<LocalizedTextToRead>> GetAllLocalizedTextsAsync(HttpClient client, Guid projectId, Guid translationKeyId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}/texts");
        return await response.Content.ReadFromJsonAsync<IEnumerable<LocalizedTextToRead>>();
    }

    public static async Task UpdateLocalizedTextAsync(HttpClient client, Guid projectId, Guid translationKeyId, Guid localizedTextId, LocalizedTextToUpdate updatedLocalizedText)
    {
        await client.PutAsJsonAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}/texts/{localizedTextId}", updatedLocalizedText);
    }

    public static async Task DeleteLocalizedTextAsync(HttpClient client, Guid projectId, Guid translationKeyId, Guid localizedTextId)
    {
        await client.DeleteAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}/texts/{localizedTextId}");
    }
}