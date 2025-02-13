using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Translations.Core.Dtos;
using Translations.Core.Services;

namespace Translations.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/projects/{projectId}/translations/keys/{translationKeyId}/texts")]
public class LocalizedTextController: ControllerBase
{
    private readonly ILocalizedTextService _localizedTextService;

    public LocalizedTextController(ILocalizedTextService localizedTextService)
    {
        _localizedTextService = localizedTextService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(Guid translationKeyId)
    {
        var localizedTexts = await _localizedTextService.GetAllAsync(translationKeyId);
        return Ok(localizedTexts);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByAsync(Guid id)
    {
        var localizedText = await _localizedTextService.GetByAsync(id);

        return Ok(localizedText);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid translationKeyId, LocalizedTextToSet localizedText)
    {
        var id = await _localizedTextService.CreateAsync(localizedText);

        return Ok(id);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, LocalizedTextToUpdate localizedText)
    {
        await _localizedTextService.UpdateAsync(id, localizedText);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _localizedTextService.DeleteAsync(id);

        return NoContent();
    }
}