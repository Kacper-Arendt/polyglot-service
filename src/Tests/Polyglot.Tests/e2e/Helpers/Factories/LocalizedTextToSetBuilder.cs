using Bogus;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class LocalizedTextToSetBuilder
{
    private readonly LocalizedTextToSet _localizedTextToSet;

    public LocalizedTextToSetBuilder(Guid languageId, Guid translationKeyId)
    {
        _localizedTextToSet = new Faker<LocalizedTextToSet>()
            .RuleFor(x => x.Value, f => f.Lorem.Sentence())
            .RuleFor(x => x.LanguageId, f => languageId)
            .RuleFor(x => x.TranslationKeyId, f => translationKeyId)
            .Generate();
    }

    public LocalizedTextToSetBuilder WithValue(string value)
    {
        _localizedTextToSet.Value = value;
        return this;
    }

    public LocalizedTextToSet Build()
    {
        return _localizedTextToSet;
    }
}