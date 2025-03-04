using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Organizations.Api.Controllers;

[ApiController]
[Route("api/" + BaseRoute)]
[Authorize]
public class OrganizationBaseController: ControllerBase
{
    private const string BaseRoute = "organizations";
}