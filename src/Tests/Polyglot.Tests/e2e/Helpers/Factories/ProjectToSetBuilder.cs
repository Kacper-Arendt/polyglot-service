using Bogus;
using Projects.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class ProjectToSetBuilder
{
    private readonly ProjectToCreateDto _project;

    public ProjectToSetBuilder()
    {
        _project = new Faker<ProjectToCreateDto>()
            .RuleFor(p => p.Name, f => f.Random.Word())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.BaseLanguage, f => f.Random.Guid())
            .Generate();
    }

    public ProjectToSetBuilder WithName(string name)
    {
        _project.Name = name;
        return this;
    }

    public ProjectToSetBuilder WithDescription(string description)
    {
        _project.Description = description;
        return this;
    }

    public ProjectToSetBuilder WithBaseLanguage(Guid baseLanguage)
    {
        _project.BaseLanguage = baseLanguage;
        return this;
    }

    public ProjectToCreateDto Build()
    {
        return _project;
    }
}