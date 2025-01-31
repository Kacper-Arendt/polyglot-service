using Projects.Core.Dtos;
using Projects.Core.Entities;
using Projects.Core.Exceptions;
using Projects.Core.Repositories;
using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private OwnerId ownerId = new OwnerId(Guid.NewGuid());

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectToReadDto> GetByAsync(ProjectId id)
    {
        var project = await _projectRepository.GetByAsync(ownerId, id);
        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        return new ProjectToReadDto(
            project.Id,
            project.Name,
            project.Description,
            project.BaseLanguage,
            project.Owner
        );
    }

    public async Task<IEnumerable<ProjectToReadDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync(ownerId);
        return projects.Select(p => new ProjectToReadDto(
            p.Id,
            p.Name,
            p.Description,
            p.BaseLanguage,
            p.Owner
        ));
    }

    public async Task<Guid> CreateAsync(ProjectToCreateDto project)
    {
        var projectId = new ProjectId(Guid.NewGuid());

        var projectEntity = Project.Create(
            projectId,
            project.Name,
            project.Description,
            project.BaseLanguage,
            ownerId
        );

        await _projectRepository.CreateAsync(projectEntity);
        return projectId.Value;
    }

    public async Task UpdateAsync(ProjectId id, ProjectToUpdateDto project)
    {
        var projectEntity = await _projectRepository.GetByAsync(ownerId, id);
        if (projectEntity is null)
        {
            throw new ProjectNotFoundException(id);
        }

        projectEntity.Update(
            project.Name,
            project.Description
        );

        await _projectRepository.UpdateAsync(projectEntity);
    }

    public async Task UpdateBaseLanguageAsync(ProjectId id, LanguageId baseLanguage)
    {
        var project = await _projectRepository.GetByAsync(ownerId, id);
        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        project.ChangeBaseLanguage(baseLanguage);
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteAsync(ProjectId id)
    {
        var project = await _projectRepository.GetByAsync(ownerId, id);

        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        await _projectRepository.DeleteAsync(id);
    }
}