using Languages.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class LanguageToSetBuilder(Guid projectId)
{
    private readonly LanguageToSetDto _language = new()
    {
        ProjectId = projectId
    };

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