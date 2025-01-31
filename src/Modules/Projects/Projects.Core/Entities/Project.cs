using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Entities;

public class Project
{
    public ProjectId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public LanguageId BaseLanguage { get; private set; }
    public OwnerId Owner { get; private set; }
    
    private Project(ProjectId id, string name, string description,  LanguageId baseLanguage, OwnerId owner)
    {
        Id = id;
        Name = name;
        Description = description;
        BaseLanguage = baseLanguage;
        Owner = owner;
    }
    
    public static Project Create(ProjectId id, string name, string description, LanguageId baseLanguage, OwnerId owner)
    {
        var project = new Project(id, name, description, baseLanguage,  owner);
        
        return project;
    }
    
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void ChangeBaseLanguage(LanguageId languageId)
    {
        BaseLanguage = languageId;
    }
}