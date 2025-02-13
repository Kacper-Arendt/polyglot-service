using Bogus;
using Translations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class TranslationKeyToSetBuilder
{
    private readonly TranslationKeyToSet _translationKeyToSet;

    public TranslationKeyToSetBuilder(Guid projectId)
    {
        _translationKeyToSet = new Faker<TranslationKeyToSet>()
            .RuleFor(x => x.ProjectId, projectId)
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .Generate();
    }

    public TranslationKeyToSetBuilder WithName(string name)
    {
        _translationKeyToSet.Name = name;
        return this;
    }
    
    public TranslationKeyToSet Build()
    {
        return _translationKeyToSet;
    }
}