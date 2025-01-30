using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Dtos;
using Users.Core.Services;

namespace Users.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        var createdUser = await _authService.RegisterUser(registerUserDto);

        return Ok(new { Id = createdUser.UserId });
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _authService.GetCurrentUserName();
        return Ok(user);
    }
}