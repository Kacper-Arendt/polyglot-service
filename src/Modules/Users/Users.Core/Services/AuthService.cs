using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Users.Core.Dtos;
using Users.Core.Entities;
using Users.Core.Exceptions;

namespace Users.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = User.CreateNormalUser(registerUserDto.Email);
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
        {
            throw new UserCreationFailedException(result.Errors.ToList());
        }

        return new RegisterUserResponseDto(user.Id);
    }

    public async Task<CurrentUserResponseDto?> GetCurrentUserName()
    {
        var name =  _httpContextAccessor.HttpContext?.User.Identity?.Name;

        return name == null ? null : new CurrentUserResponseDto(name);
    }
}