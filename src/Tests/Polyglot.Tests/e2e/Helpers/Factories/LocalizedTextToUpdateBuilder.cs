using Bogus;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class LocalizedTextToUpdateBuilder
{
    private readonly LocalizedTextToUpdate _localizedTextToSet;

    public LocalizedTextToUpdateBuilder()
    {
        _localizedTextToSet = new Faker<LocalizedTextToUpdate>()
            .RuleFor(x => x.Value, f => f.Lorem.Sentence())
            .Generate();
    }

    public LocalizedTextToUpdateBuilder WithValue(string value)
    {
        _localizedTextToSet.Value = value;
        return this;
    }

    public LocalizedTextToUpdate Build()
    {
        return _localizedTextToSet;
    }
}