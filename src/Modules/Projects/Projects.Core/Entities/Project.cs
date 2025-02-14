namespace Projects.Core.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid? BaseLanguage { get; private set; }
    public Guid Owner { get; private set; }
    
    private Project(Guid id, string name, string description, Guid owner)
    {
        Id = id;
        Name = name;
        Description = description;
        BaseLanguage = null;
        Owner = owner;
    }
    
    public static Project Create(Guid id, string name, string description, Guid owner)
    {
        var project = new Project(id, name, description,  owner);
        
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