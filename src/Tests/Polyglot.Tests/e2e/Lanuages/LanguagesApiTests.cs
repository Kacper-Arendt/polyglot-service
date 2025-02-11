using System.Net.Http.Json;
using System.Text.Json;
using Languages.Core.Dtos;
using Polyglot.Tests.e2e.Setup;

public class LanguagesApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;
    private readonly Guid _projectId = Guid.Parse("0f78f536-41c2-4561-b754-0d19d31ec450");

    public LanguagesApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task CreateLangs()
    {
        // Arrange
        var languageToSetDto = new LanguageToSetDto
        {
            Code = "TEST_CODE",
            Name = "TEST_LANGUAGE",
            ProjectId = _projectId
        };
        
        var body = JsonContent.Create(languageToSetDto);
        
        // Act
        var response = await _client.PostAsync($"/api/projects/{_projectId}/languages", body);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        // Odczytaj ID nowo utworzonego języka
        var createdLanguageId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdLanguageId);

        // Weryfikacja, czy język został faktycznie dodany
        var getResponse = await _client.GetAsync($"/api/projects/{_projectId}/languages");
        var allLanguages = await getResponse.Content.ReadFromJsonAsync<IEnumerable<LanguageToReadDto>>();
        
        Assert.Contains(allLanguages, lang => lang.Id == createdLanguageId);
    }

    [Fact]
    public async Task GetAllLangs()
    {
        // Act
        var response = await _client.GetAsync($"/api/projects/{_projectId}/languages");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<LanguageToReadDto>>();
        Assert.NotEmpty(responseBody);
    }
}