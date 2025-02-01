using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Projects.Core.Dtos;
using Projects.Core.Entities;
using Projects.Core.Exceptions;
using Projects.Core.Repositories;
using Shared.Infrastructure.Helpers;

namespace Projects.Core.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly HttpContextHelper _httpContextHelper;

    public ProjectService(IProjectRepository projectRepository, HttpContextHelper httpContextHelper)
    {
        _projectRepository = projectRepository;
        _httpContextHelper = httpContextHelper;
    }
    
    public async Task<ProjectToReadDto> GetByAsync(Guid id)
    {
        var ownerId = _httpContextHelper.GetCurrentUserId();
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
        var ownerId = _httpContextHelper.GetCurrentUserId();
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
        var ownerId = _httpContextHelper.GetCurrentUserId();
        var projectId = Guid.NewGuid();

        var projectEntity = Project.Create(
            projectId,
            project.Name,
            project.Description,
            project.BaseLanguage,
            ownerId
        );

        await _projectRepository.CreateAsync(projectEntity);
        return projectId;
    }

    public async Task UpdateAsync(Guid id, ProjectToUpdateDto project)
    {
        var ownerId = _httpContextHelper.GetCurrentUserId();
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

    public async Task UpdateBaseLanguageAsync(Guid id, Guid baseLanguage)
    {
        var ownerId = _httpContextHelper.GetCurrentUserId();
        var project = await _projectRepository.GetByAsync(ownerId, id);
        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        project.ChangeBaseLanguage(baseLanguage);
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteAsync(Guid id)
    {
        var ownerId = _httpContextHelper.GetCurrentUserId();
        var project = await _projectRepository.GetByAsync(ownerId, id);

        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        await _projectRepository.DeleteAsync(id);
    }
}