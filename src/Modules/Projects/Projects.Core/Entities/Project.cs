namespace Projects.Core.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid BaseLanguage { get; private set; }
    public Guid Owner { get; private set; }
    
    private Project(Guid id, string name, string description,  Guid baseLanguage, Guid owner)
    {
        Id = id;
        Name = name;
        Description = description;
        BaseLanguage = baseLanguage;
        Owner = owner;
    }
    
    public static Project Create(Guid id, string name, string description, Guid baseLanguage, Guid owner)
    {
        var project = new Project(id, name, description, baseLanguage,  owner);
        
        return project;
    }
    
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void ChangeBaseLanguage(Guid languageId)
    {
        BaseLanguage = languageId;
    }
}