using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/"+ BasePath +"/[controller]")]
public abstract class BaseController: ControllerBase
{
    internal const string BasePath = "Users";
}