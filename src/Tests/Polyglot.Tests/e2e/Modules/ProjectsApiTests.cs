using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class ProjectsApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public ProjectsApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }

    [Fact]
    public async Task CreateProject_ShouldReturnNewProjectId()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();

        // Act
        var response = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);

        // Assert
        Assert.NotEqual(Guid.Empty, response);
    }

    [Fact]
    public async Task GetAllProjects_ShouldReturnEmptyInitially()
    {
        // Act
        var response = await ProjectHelper.GetAllProjectsAsync(_client);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    [Fact]
    public async Task GetAllProjects_ShouldReturnProjects()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var createdProjectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);

        // Act
        var response = await ProjectHelper.GetAllProjectsAsync(_client);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Contains(response, p => p.Id == createdProjectId);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectProject()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var createdProjectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);

        // Act
        var retrievedProject = await ProjectHelper.GetProjectByIdAsync(_client, createdProjectId);

        // Assert
        Assert.NotNull(retrievedProject);
        Assert.Equal(projectToSetDto.Name, retrievedProject.Name);
        Assert.Equal(projectToSetDto.Description, retrievedProject.Description);
    }

    [Fact]
    public async Task UpdateProject_ShouldModifyExistingProject()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var createdProjectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);
        var updatedProject = new ProjectToUpdateBuilder().Build();

        // Act
        await ProjectHelper.UpdateProjectAsync(_client, createdProjectId, updatedProject);
        var retrievedProject = await ProjectHelper.GetProjectByIdAsync(_client, createdProjectId);

        // Assert
        Assert.NotNull(retrievedProject);
        Assert.Equal(updatedProject.Name, retrievedProject.Name);
        Assert.Equal(updatedProject.Description, retrievedProject.Description);
    }

    [Fact]
    public async Task DeleteProject_ShouldRemoveProject()
    {
        // Arrange
        var projectToSetDto = new ProjectToSetBuilder().Build();
        var createdProjectId = await ProjectHelper.CreateProjectAsync(_client, projectToSetDto);

        // Act
        await ProjectHelper.DeleteProjectAsync(_client, createdProjectId);
        var retrievedProject = await ProjectHelper.GetProjectByIdAsync(_client, createdProjectId);

        // Assert
        Assert.Null(retrievedProject);
    }
}