using Bogus;
using Languages.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class LanguageToSetBuilder
{
    private readonly LanguageToSetDto _language;

    public LanguageToSetBuilder(Guid projectId)
    {
        _language = new Faker<LanguageToSetDto>()
            .RuleFor(l => l.ProjectId, projectId)
            .RuleFor(l => l.Name, f => f.Random.Word())
            .RuleFor(l => l.Code, f => f.Random.AlphaNumeric(5))
            .Generate();
    }

    public LanguageToSetBuilder WithName(string name)
    {
        _language.Name = name;
        return this;
    }

    public LanguageToSetBuilder WithCode(string code)
    {
        _language.Code = code;
        return this;
    }

    public LanguageToSetDto Build()
    {
        return _language;
    }
}