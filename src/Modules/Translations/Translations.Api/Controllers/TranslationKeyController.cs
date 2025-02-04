using Microsoft.AspNetCore.Mvc;
using Translations.Core.Dtos;
using Translations.Core.Services;

namespace Translations.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId}/translations/keys")]
public class TranslationKeyController : ControllerBase
{
    private readonly ITranslationKeyService _translationKeyService;

    public TranslationKeyController(ITranslationKeyService translationKeyService)
    {
        _translationKeyService = translationKeyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(Guid projectId)
    {
        var translationKeys = await _translationKeyService.GetAllAsync(projectId);
        return Ok(translationKeys);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByAsync(Guid id)
    {
        var translationKey = await _translationKeyService.GetByAsync(id);

        return Ok(translationKey);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid projectId, TranslationKeyToSet translationKey)
    {
        var id = await _translationKeyService.CreateAsync(translationKey);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, TranslationKeyToUpdate translationKey)
    {
        await _translationKeyService.UpdateAsync(id, translationKey);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _translationKeyService.DeleteAsync(id);

        return NoContent();
    }
}