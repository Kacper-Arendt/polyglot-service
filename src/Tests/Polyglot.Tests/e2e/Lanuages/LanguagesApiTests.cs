using System.Net.Http.Json;
using Languages.Core.Dtos;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Languages;

public class LanguagesApiTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task CreateLang_ShouldReturnNewLanguageId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetDto
        {
            Code = "TEST_CODE",
            Name = "TEST_LANGUAGE",
            ProjectId = projectId
        };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/projects/{projectId}/languages", languageToSetDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdLanguageId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdLanguageId);
    }

    [Fact]
    public async Task GetAllLangs_ShouldReturnEmptyInitially()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/projects/{projectId}/languages");
        response.EnsureSuccessStatusCode();
        var allLanguages = await response.Content.ReadFromJsonAsync<IEnumerable<LanguageToReadDto>>();

        // Assert
        Assert.NotNull(allLanguages);
        Assert.Empty(allLanguages);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectLanguage()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetDto
        {
            Code = "TEST_CODE",
            Name = "TEST_LANGUAGE",
            ProjectId = projectId
        };

        var createResponse = await _client.PostAsJsonAsync($"/api/projects/{projectId}/languages", languageToSetDto);
        createResponse.EnsureSuccessStatusCode();
        var createdLanguageId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.GetAsync($"/api/projects/{projectId}/languages/{createdLanguageId}");
        response.EnsureSuccessStatusCode();
        var retrievedLanguage = await response.Content.ReadFromJsonAsync<LanguageToReadDto>();

        // Assert
        Assert.NotNull(retrievedLanguage);
        Assert.Equal(languageToSetDto.Name, retrievedLanguage.Name);
        Assert.Equal(languageToSetDto.Code, retrievedLanguage.Code);
    }

    [Fact]
    public async Task UpdateLang_ShouldModifyExistingLanguage()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var initialLanguage = new LanguageToSetDto
        {
            Code = "TEST_CODE",
            Name = "TEST_LANGUAGE",
            ProjectId = projectId
        };

        var createResponse = await _client.PostAsJsonAsync($"/api/projects/{projectId}/languages", initialLanguage);
        createResponse.EnsureSuccessStatusCode();
        var createdLanguageId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var updatedLanguage = new LanguageToSetDto
        {
            Code = "UPDATED_CODE",
            Name = "UPDATED_LANGUAGE",
            ProjectId = projectId
        };

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/api/projects/{projectId}/languages/{createdLanguageId}", updatedLanguage);
        updateResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"/api/projects/{projectId}/languages/{createdLanguageId}");
        var retrievedLanguage = await getResponse.Content.ReadFromJsonAsync<LanguageToReadDto>();

        // Assert
        Assert.NotNull(retrievedLanguage);
        Assert.Equal(updatedLanguage.Name, retrievedLanguage.Name);
        Assert.Equal(updatedLanguage.Code, retrievedLanguage.Code);
    }

    [Fact]
    public async Task DeleteLang_ShouldRemoveLanguage()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetDto
        {
            Code = "DELETE_ME",
            Name = "DELETE_ME",
            ProjectId = projectId
        };

        var createResponse = await _client.PostAsJsonAsync($"/api/projects/{projectId}/languages", languageToSetDto);
        createResponse.EnsureSuccessStatusCode();
        var createdLanguageId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/projects/{projectId}/languages/{createdLanguageId}");
        deleteResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"/api/projects/{projectId}/languages/{createdLanguageId}");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, getResponse.StatusCode);
    }
}
