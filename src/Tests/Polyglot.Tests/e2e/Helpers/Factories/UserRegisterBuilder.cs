using Bogus;
using Users.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class UserRegisterBuilder
{
    private RegisterUserDto _user;

    public UserRegisterBuilder()
    {
        var faker = new Faker();
        _user = new RegisterUserDto(faker.Internet.Email(), faker.Internet.Password(20, false, "", "1@Aa#"));
    }

    public UserRegisterBuilder WithEmail(string email)
    {
        return new UserRegisterBuilder
        {
            _user = _user with { Email = email }
        };
    }

    public UserRegisterBuilder WithPassword(string password)
    {
        return new UserRegisterBuilder
        {
            _user = _user with { Password = password }
        };
    }

    public RegisterUserDto Build()
    {
        return _user;
    }
}