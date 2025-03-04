using Bogus;
using Organizations.Core.Dtos;

namespace Polyglot.Tests.e2e.Helpers.Factories;

public class OrganizationToUpdateBuilder
{
  private readonly OrganizationToSetDto _organization;

  public OrganizationToUpdateBuilder()
  {
    _organization = new Faker<OrganizationToSetDto>()
      .RuleFor(x => x.Name, f => f.Company.CompanyName());
  }
  
  public OrganizationToUpdateBuilder WithName(string name)
  {
    _organization.Name = name;
    return this;
  }
  
  public OrganizationToSetDto Build()
  {
    return _organization;
  }
}