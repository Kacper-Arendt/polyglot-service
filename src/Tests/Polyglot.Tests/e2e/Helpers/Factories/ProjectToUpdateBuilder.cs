using Bogus;
using Projects.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class ProjectToUpdateBuilder
{
    private readonly ProjectToUpdateDto _project;

    public ProjectToUpdateBuilder()
    {
        _project = new Faker<ProjectToUpdateDto>()
            .RuleFor(p => p.Name, f => f.Random.Word())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .Generate();
    }

    public ProjectToUpdateBuilder WithName(string name)
    {
        _project.Name = name;
        return this;
    }

    public ProjectToUpdateBuilder WithDescription(string description)
    {
        _project.Description = description;
        return this;
    }

    public ProjectToUpdateDto Build()
    {
        return _project;
    }
}