using Users.Core.Dtos;

namespace Users.Core.Services;

public interface IAuthService
{
    Task<RegisterUserResponseDto> RegisterUser(RegisterUserDto registerUserDto);
    Task<CurrentUserResponseDto?> GetCurrentUserName();
}