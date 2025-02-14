using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class LocalizedTextApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public LocalizedTextApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }

    [Fact]
    public async Task CreateLocalizedText_ShouldReturnNewLocalizedTextId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();
        var localizedTextToSetDto = new LocalizedTextToSetBuilder(projectId, translationKeyId).Build();

        // Act
        var response = await LocalizedTextHelper.CreateLocalizedTextAsync(_client, projectId, translationKeyId, localizedTextToSetDto);

        // Assert
        Assert.NotEqual(Guid.Empty, response);
    }

    [Fact]
    public async Task GetAllLocalizedTexts_ShouldReturnEmptyInitially()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();

        // Act
        var response = await LocalizedTextHelper.GetAllLocalizedTextsAsync(_client, projectId, translationKeyId);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    [Fact]
    public async Task GetAllLocalizedTexts_ShouldReturnLocalizedTexts()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();
        var localizedTextToSetDto = new LocalizedTextToSetBuilder(projectId, translationKeyId).Build();
        var createdLocalizedTextId = await LocalizedTextHelper.CreateLocalizedTextAsync(_client, projectId, translationKeyId, localizedTextToSetDto);

        // Act
        var response = await LocalizedTextHelper.GetAllLocalizedTextsAsync(_client, projectId, translationKeyId);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Contains(response, t => t.Id == createdLocalizedTextId);
    }

    [Fact]
    public async Task GetLocalizedTextById_ShouldReturnCorrectLocalizedText()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();
        var localizedTextToSetDto = new LocalizedTextToSetBuilder(projectId, translationKeyId).Build();
        var createdLocalizedTextId = await LocalizedTextHelper.CreateLocalizedTextAsync(_client, projectId, translationKeyId, localizedTextToSetDto);

        // Act
        var retrievedLocalizedText = await LocalizedTextHelper.GetLocalizedTextByIdAsync(_client, projectId, translationKeyId, createdLocalizedTextId);

        // Assert
        Assert.NotNull(retrievedLocalizedText);
        Assert.Equal(localizedTextToSetDto.Value, retrievedLocalizedText.Value);
    }

    [Fact]
    public async Task UpdateLocalizedText_ShouldModifyExistingLocalizedText()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();
        var localizedTextToSetDto = new LocalizedTextToSetBuilder(projectId, translationKeyId).Build();
        var createdLocalizedTextId = await LocalizedTextHelper.CreateLocalizedTextAsync(_client, projectId, translationKeyId, localizedTextToSetDto);

        var updatedLocalizedText = new LocalizedTextToUpdateBuilder()
            .WithValue("UPDATED_LOCALIZED_TEXT")
            .Build();

        // Act
        await LocalizedTextHelper.UpdateLocalizedTextAsync(_client, projectId, translationKeyId, createdLocalizedTextId, updatedLocalizedText);
        var retrievedLocalizedText = await LocalizedTextHelper.GetLocalizedTextByIdAsync(_client, projectId, translationKeyId, createdLocalizedTextId);

        // Assert
        Assert.NotNull(retrievedLocalizedText);
        Assert.Equal(updatedLocalizedText.Value, retrievedLocalizedText.Value);
    }

    [Fact]
    public async Task DeleteLocalizedText_ShouldRemoveLocalizedText()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var translationKeyId = Guid.NewGuid();
        var localizedTextToSetDto = new LocalizedTextToSetBuilder(projectId, translationKeyId).Build();
        var createdLocalizedTextId = await LocalizedTextHelper.CreateLocalizedTextAsync(_client, projectId, translationKeyId, localizedTextToSetDto);

        // Act
        await LocalizedTextHelper.DeleteLocalizedTextAsync(_client, projectId, translationKeyId, createdLocalizedTextId);
        var retrievedLocalizedText = await LocalizedTextHelper.GetLocalizedTextByIdAsync(_client, projectId, translationKeyId, createdLocalizedTextId);

        // Assert
        Assert.Null(retrievedLocalizedText);
    }
}