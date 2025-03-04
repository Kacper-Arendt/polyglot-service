using Microsoft.AspNetCore.Mvc;
using Organizations.Core.Services.Queries;

namespace Organizations.Api.Controllers;

public class OrganizationQueryController: OrganizationBaseController
{
    private readonly IOrganizationQueryService _organizationQueryService;

    public OrganizationQueryController(IOrganizationQueryService organizationQueryService)
    {
        _organizationQueryService = organizationQueryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var organizations = await _organizationQueryService.GetAllAsync();
        return Ok(organizations);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var organization = await _organizationQueryService.GetAsync(id);
        
        if (organization is null)
        {
            return NotFound();
        }
        
        return Ok(organization);
    }
}