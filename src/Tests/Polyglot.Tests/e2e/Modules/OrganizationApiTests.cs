using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class OrganizationApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public OrganizationApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }

    [Fact]
    public async Task CreateOrganization_ShouldReturnNewOrganizationId()
    {
        // Arrange
        var organizationToSetDto = new OrganizationToSetBuilder().Build();

        // Act
        var response = await OrganizationHelper.CreateOrganizationAsync(_client, organizationToSetDto);

        // Assert
        Assert.NotEqual(Guid.Empty, response);
    }

    [Fact]
    public async Task GetAllOrganizations_ShouldReturnEmptyInitially()
    {
        // Act
        var response = await OrganizationHelper.GetAllOrganizationsAsync(_client);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    [Fact]
    public async Task GetAllOrganizations_ShouldReturnOrganizations()
    {
        // Arrange
        var organizationToSetDto = new OrganizationToSetBuilder().Build();
        var createdOrganizationId = await OrganizationHelper.CreateOrganizationAsync(_client, organizationToSetDto);

        // Act
        var response = await OrganizationHelper.GetAllOrganizationsAsync(_client);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Contains(response, o => o.Id == createdOrganizationId);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectOrganization()
    {
        // Arrange
        var organizationToSetDto = new OrganizationToSetBuilder().Build();
        var createdOrganizationId = await OrganizationHelper.CreateOrganizationAsync(_client, organizationToSetDto);

        // Act
        var retrievedOrganization = await OrganizationHelper.GetOrganizationByIdAsync(_client, createdOrganizationId);

        // Assert
        Assert.NotNull(retrievedOrganization);
        Assert.Equal(organizationToSetDto.Name, retrievedOrganization.Name);
    }

    [Fact]
    public async Task UpdateOrganization_ShouldModifyExistingOrganization()
    {
        // Arrange
        var organizationToSetDto = new OrganizationToSetBuilder().Build();
        var createdOrganizationId = await OrganizationHelper.CreateOrganizationAsync(_client, organizationToSetDto);
        var updatedOrganization = new OrganizationToUpdateBuilder().Build();

        // Act
        await OrganizationHelper.UpdateOrganizationAsync(_client, createdOrganizationId, updatedOrganization);
        var retrievedOrganization = await OrganizationHelper.GetOrganizationByIdAsync(_client, createdOrganizationId);

        // Assert
        Assert.NotNull(retrievedOrganization);
        Assert.Equal(updatedOrganization.Name, retrievedOrganization.Name);
    }

    [Fact]
    public async Task DeleteOrganization_ShouldRemoveOrganization()
    {
        // Arrange
        var organizationToSetDto = new OrganizationToSetBuilder().Build();
        var createdOrganizationId = await OrganizationHelper.CreateOrganizationAsync(_client, organizationToSetDto);

        // Act
        await OrganizationHelper.DeleteOrganizationAsync(_client, createdOrganizationId);
        var retrievedOrganization = await OrganizationHelper.GetOrganizationByIdAsync(_client, createdOrganizationId);

        // Assert
        Assert.Null(retrievedOrganization);
    }
}