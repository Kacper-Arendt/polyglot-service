using System.Net.Http.Json;
using Languages.Core.Dtos;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Lanuages;

public class LanguagesApiTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task CreateLangs()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetDto
        {
            Code = "TEST_CODE",
            Name = "TEST_LANGUAGE",
            ProjectId =projectId
        };
        
        var body = JsonContent.Create(languageToSetDto);
        
        // Act
        var response = await _client.PostAsync($"/api/projects/{projectId}/languages", body);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var createdLanguageId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdLanguageId);

        var getResponse = await _client.GetAsync($"/api/projects/{projectId}/languages");
        var allLanguages = await getResponse.Content.ReadFromJsonAsync<IEnumerable<LanguageToReadDto>>();
        
        Assert.Contains(allLanguages, lang => lang.Id == createdLanguageId);
    }

    [Fact]
    public async Task GetAllLangs()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"/api/projects/{projectId}/languages");
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
}