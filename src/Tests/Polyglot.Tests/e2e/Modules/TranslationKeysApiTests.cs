using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class TranslationKeysApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public TranslationKeysApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }

    [Fact]
    public async Task CreateTranslationKey_ShouldReturnNewTranslationKeyId()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var projectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var translationKeyToSetDto = new TranslationKeyToSetBuilder(projectId).Build();

        // Act
        var response = await TranslationHelper.CreateTranslationKeyAsync(_client, projectId, translationKeyToSetDto);

        // Assert
        Assert.NotEqual(Guid.Empty, response);
    }

    [Fact]
    public async Task GetAllTranslationKeys_ShouldReturnEmptyInitially()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        // Act
        var response = await TranslationHelper.GetAllTranslationKeysAsync(_client, projectId);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    [Fact]
    public async Task GetAllTranslationKeys_ShouldReturnTranslationKeys()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var projectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var translationKeyToSetDto = new TranslationKeyToSetBuilder(projectId).Build();
        var createdTranslationKeyId = await TranslationHelper.CreateTranslationKeyAsync(_client, projectId, translationKeyToSetDto);

        // Act
        var response = await TranslationHelper.GetAllTranslationKeysAsync(_client, projectId);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Contains(response, k => k.Id == createdTranslationKeyId);
    }

    [Fact]
    public async Task GetTranslationKeyById_ShouldReturnCorrectTranslationKey()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var projectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var translationKeyToSetDto = new TranslationKeyToSetBuilder(projectId).Build();
        var createdTranslationKeyId = await TranslationHelper.CreateTranslationKeyAsync(_client, projectId, translationKeyToSetDto);

        // Act
        var retrievedTranslationKey = await TranslationHelper.GetTranslationKeyByIdAsync(_client, projectId, createdTranslationKeyId);

        // Assert
        Assert.NotNull(retrievedTranslationKey);
        Assert.Equal(translationKeyToSetDto.Name, retrievedTranslationKey.Name);
    }

    [Fact]
    public async Task UpdateTranslationKey_ShouldModifyExistingTranslationKey()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var projectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var translationKeyToSetDto = new TranslationKeyToSetBuilder(projectId).Build();
        var createdTranslationKeyId = await TranslationHelper.CreateTranslationKeyAsync(_client, projectId, translationKeyToSetDto);

        var updatedTranslationKey = new TranslationKeyToUpdateBuilder()
            .WithName("UPDATED_TRANSLATION_KEY")
            .Build();

        // Act
        await TranslationHelper.UpdateTranslationKeyAsync(_client, projectId, createdTranslationKeyId, updatedTranslationKey);
        var retrievedTranslationKey = await TranslationHelper.GetTranslationKeyByIdAsync(_client, projectId, createdTranslationKeyId);

        // Assert
        Assert.NotNull(retrievedTranslationKey);
        Assert.Equal(updatedTranslationKey.Name, retrievedTranslationKey.Name);
    }

    [Fact]
    public async Task DeleteTranslationKey_ShouldRemoveTranslationKey()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var projectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var translationKeyToSetDto = new TranslationKeyToSetBuilder(projectId).Build();
        var createdTranslationKeyId = await TranslationHelper.CreateTranslationKeyAsync(_client, projectId, translationKeyToSetDto);

        // Act
        await TranslationHelper.DeleteTranslationKeyAsync(_client, projectId, createdTranslationKeyId);
        var retrievedTranslationKey = await TranslationHelper.GetTranslationKeyByIdAsync(_client, projectId, createdTranslationKeyId);

        // Assert
        Assert.Null(retrievedTranslationKey);
    }
}