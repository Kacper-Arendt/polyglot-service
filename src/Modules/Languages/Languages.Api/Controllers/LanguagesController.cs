using Languages.Core.Dtos;
using Languages.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Languages.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId}/languages")]
public class LanguagesController : ControllerBase
{
    private readonly ILanguagesService _languagesService;

    public LanguagesController(ILanguagesService languagesService)
    {
        _languagesService = languagesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(Guid projectId)
    {
        var languages = await _languagesService.GetAllAsync(projectId);
        return Ok(languages);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByAsync(Guid projectId, Guid id)
    {
        var language = await _languagesService.GetByAsync(id);
        return Ok(language);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(LanguageToSetDto language)
    {
        var id = await _languagesService.CreateAsync( language);
        return Ok(id);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, LanguageToSetDto language)
    {
        await _languagesService.UpdateAsync(id, language);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _languagesService.DeleteAsync(id);
        return NoContent();
    }
}