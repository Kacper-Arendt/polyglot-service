using Bogus;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class TranslationKeyToUpdateBuilder
{
    private readonly TranslationKeyToUpdate _translationKeyToUpdate;

    public TranslationKeyToUpdateBuilder()
    {
        _translationKeyToUpdate = new Faker<TranslationKeyToUpdate>()
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .Generate();
    }

    public TranslationKeyToUpdateBuilder WithName(string name)
    {
        _translationKeyToUpdate.Name = name;
        return this;
    }
    
    public TranslationKeyToUpdate Build()
    {
        return _translationKeyToUpdate;
    }
}