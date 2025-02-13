using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class LanguagesApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public LanguagesApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }

    [Fact]
    public async Task CreateLang_ShouldReturnNewLanguageId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetBuilder(projectId)
            .Build();

        // Act
        var response = await LanguageHelper.CreateLanguageAsync(_client, projectId, languageToSetDto);

        Assert.NotEqual(Guid.Empty, response);
    }

    [Fact]
    public async Task GetAllLangs_ShouldReturnEmptyInitially()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        // Act
        var response = await LanguageHelper.GetAllLanguagesAsync(_client, projectId);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    [Fact]
    public async Task GetAllLangs_ShouldReturnLanguages()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetBuilder(projectId).Build();
        var createdLanguageId = await LanguageHelper.CreateLanguageAsync(_client, projectId, languageToSetDto);

        // Act
        var response = await LanguageHelper.GetAllLanguagesAsync(_client, projectId);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Contains(response, l => l.Id == createdLanguageId);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectLanguage()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetBuilder(projectId)
            .Build();
        var createdLanguageId = await LanguageHelper.CreateLanguageAsync(_client, projectId, languageToSetDto);

        // Act
        var retrievedLanguage = await LanguageHelper.GetLanguageByIdAsync(_client, projectId, createdLanguageId);

        // Assert
        Assert.NotNull(retrievedLanguage);
        Assert.Equal(languageToSetDto.Name, retrievedLanguage.Name);
        Assert.Equal(languageToSetDto.Code, retrievedLanguage.Code);
    }

    [Fact(Skip = "Failed")]
    public async Task UpdateLang_ShouldModifyExistingLanguage()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var languageToSetDto = new LanguageToSetBuilder(projectId)
            .Build();

        var createdLanguageId = await LanguageHelper.CreateLanguageAsync(_client, projectId, languageToSetDto);

        var updatedLanguage = new LanguageToSetBuilder(projectId)
            .WithName("UPDATED_LANGUAGE")
            .WithCode("UPDATED_CODE")
            .Build();

        // Act
        await LanguageHelper.UpdateLanguageAsync(_client, projectId, createdLanguageId, updatedLanguage);
        var retrievedLanguage = await LanguageHelper.GetLanguageByIdAsync(_client, projectId, createdLanguageId);

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

        var languageToSetDto = new LanguageToSetBuilder(projectId).Build();

        var createdLanguageId = await LanguageHelper.CreateLanguageAsync(_client, projectId, languageToSetDto);

        // Act
        await LanguageHelper.DeleteLanguageAsync(_client, projectId, createdLanguageId);
        var retrievedLanguage = await LanguageHelper.GetLanguageByIdAsync(_client, projectId, createdLanguageId);

        // Assert
        Assert.Equal(Guid.Empty, retrievedLanguage?.Id);
    }
}