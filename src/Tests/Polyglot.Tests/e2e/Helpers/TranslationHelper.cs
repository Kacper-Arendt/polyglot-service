using System.Net.Http.Json;
using Polyglot.Tests.e2e.Helpers.Factories;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public static class TranslationHelper
{
    public static async Task<Guid> CreateTranslationKeyAsync(HttpClient client, Guid projectId, TranslationKeyToSet translationKeyToSetDto)
    {
        var response = await client.PostAsJsonAsync($"/api/projects/{projectId}/translations/keys", translationKeyToSetDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public static async Task<TranslationKeyToRead?> GetTranslationKeyByIdAsync(HttpClient client, Guid projectId, Guid translationKeyId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}");
        return await response.Content.ReadFromJsonAsync<TranslationKeyToRead>();
    }

    public static async Task<IEnumerable<TranslationKeyToRead>> GetAllTranslationKeysAsync(HttpClient client, Guid projectId)
    {
        var response = await client.GetAsync($"/api/projects/{projectId}/translations/keys");
        return await response.Content.ReadFromJsonAsync<IEnumerable<TranslationKeyToRead>>();
    }

    public static async Task UpdateTranslationKeyAsync(HttpClient client, Guid projectId, Guid translationKeyId, TranslationKeyToUpdate updatedTranslationKey)
    {
        await client.PutAsJsonAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}", updatedTranslationKey);
    }

    public static async Task DeleteTranslationKeyAsync(HttpClient client, Guid projectId, Guid translationKeyId)
    {
        await client.DeleteAsync($"/api/projects/{projectId}/translations/keys/{translationKeyId}");
    }
}