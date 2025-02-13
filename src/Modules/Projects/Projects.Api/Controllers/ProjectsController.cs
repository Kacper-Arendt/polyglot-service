using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projects.Core.Dtos;
using Projects.Core.Services;

namespace Projects.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] string? name)
    {
        var projects = await _projectService.GetAllAsync(name);
        return Ok(projects);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await _projectService.GetByAsync(id);
        return Ok(project);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProject(ProjectToCreateDto projectToCreate)
    {
        var projectId = await _projectService.CreateAsync(projectToCreate);
        return Ok(projectId);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(Guid id, ProjectToUpdateDto projectToUpdate)
    {
        await _projectService.UpdateAsync(id, projectToUpdate);
        return Ok();
    }
    
    [HttpPut("{id}/base-language")]
    public async Task<ActionResult> UpdateBaseLanguage(Guid id, ProjectLanguageToUpdateDto languageToUpdate)
    {
        await _projectService.UpdateBaseLanguageAsync(id, languageToUpdate.LanguageId);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        await _projectService.DeleteAsync(id);
        return Ok();
    }
}