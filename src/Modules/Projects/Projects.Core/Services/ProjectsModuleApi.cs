using Projects.Core.Repositories;
using Projects.Shared;
using Projects.Shared.Dto;

namespace Projects.Core.Services;

public class ProjectsModuleApi : IProjectsModuleApi
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsModuleApi(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<ProjectDto?> GetAsync(Guid id, Guid ownerId)
    {
        var project = await _projectRepository.GetByAsync(ownerId, id);
        
        var projectDto = project is null
            ? null
            : new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.BaseLanguage,
                project.Owner
            );
        
        return projectDto;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _projectRepository.ExistsAsync(id);
    }
}