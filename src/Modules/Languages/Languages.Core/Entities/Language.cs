namespace Languages.Core.Entities;

public class Language
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public Guid ProjectId { get; private set; }
    
    private Language(Guid id, string name, string code, Guid projectId)
    {
        Id = id;
        Name = name;
        Code = code;
        ProjectId = projectId;
    }
    
    public static Language Create(Guid id, string name, string code,  Guid projectId)
    {
        var language = new Language(id, name, code, projectId);
        
        return language;
    }
    
    public void Update(string name, string code)
    {
        Name = name;
        Code = code;
    }
}