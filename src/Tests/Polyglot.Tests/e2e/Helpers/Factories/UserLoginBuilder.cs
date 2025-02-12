namespace Polyglot.Tests.e2e.Helpers.Factories;

public class UserLoginBuilder
{
    private readonly LoginUserDto _user;

    public UserLoginBuilder()
    {
        _user = new LoginUserDto();
    }

    public UserLoginBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserLoginBuilder WithPassword(string password)
    {
        _user.Password = password;
        return this;
    }

    public LoginUserDto Build()
    {
        return _user;
    }
}

public class LoginUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}