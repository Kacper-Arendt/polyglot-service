using Microsoft.AspNetCore.Mvc;
using Organizations.Core.Dtos;
using Organizations.Core.Services.Commands;

namespace Organizations.Api.Controllers;

public class OrganizationCommandController: OrganizationBaseController
{
    private readonly IOrganizationCommandService _organizationCommandService;

    public OrganizationCommandController(IOrganizationCommandService organizationCommandService)
    {
        _organizationCommandService = organizationCommandService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrganizationToSetDto organization)
    {
        var id = await _organizationCommandService.AddAsync(organization);
        return Ok(id);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, OrganizationToSetDto organization)
    {
        await _organizationCommandService.UpdateAsync(id, organization);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _organizationCommandService.DeleteAsync(id);
        return NoContent();
    }
}