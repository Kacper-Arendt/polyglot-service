using System.Net.Http.Json;
using Languages.Core.Dtos;
using Polyglot.Tests.e2e.Helpers.Factories;
using Users.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers;

public static class AuthHelper
{
    public static async Task<HttpResponseMessage> RegisterAsync(HttpClient client, RegisterUserDto registerUserDto)
    {
        return await client.PostAsJsonAsync($"/api/Users/Auth/Register", registerUserDto);
    }

    public static async Task<LoginResponseDto> LoginAsync(HttpClient client, LoginUserDto loginUserDto)
    {
        var response = await client.PostAsJsonAsync($"/api/Auth/Login", loginUserDto);
        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }
}

public class LoginResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; }
}