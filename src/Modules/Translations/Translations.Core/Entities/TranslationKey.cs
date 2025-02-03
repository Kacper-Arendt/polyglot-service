namespace Translations.Core.Entities;

public class TranslationKey
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProjectId { get; private set; }
    
    private TranslationKey(string name, Guid projectId)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProjectId = projectId;
    }
    
    public static TranslationKey Create(string name, Guid projectId)
    {
        return new TranslationKey(name, projectId);
    }
    
    public void Update(string name)
    {
        Name = name;
    }
}