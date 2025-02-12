using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Helpers.Factories;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class UsersApiTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();


    [Fact]
    public async Task CreateUser_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        var userToRegisterDto = new UserRegisterBuilder()
            .Build();

        // Act
        var response = await AuthHelper.RegisterAsync(_client, userToRegisterDto);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task LoginUser_ShouldReturnToken()
    {
        // Arrange
        var userToRegisterDto = new UserRegisterBuilder()
            .Build();

        var registerResponse = await AuthHelper.RegisterAsync(_client, userToRegisterDto);
        registerResponse.EnsureSuccessStatusCode();

        var userToLoginDto = new UserLoginBuilder()
            .WithEmail(userToRegisterDto.Email)
            .WithPassword(userToRegisterDto.Password)
            .Build();

        // Act
        var loginResponse = await AuthHelper.LoginAsync(_client, userToLoginDto);

        // Assert
        Assert.NotNull(loginResponse.AccessToken);
        Assert.NotNull(loginResponse.RefreshToken);
        Assert.Equal(3600, loginResponse.ExpiresIn);
        Assert.NotNull(loginResponse.TokenType);
    }
}