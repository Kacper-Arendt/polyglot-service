namespace Organizations.Core.Entities;

public class Organization
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Organization(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static Organization Create(Guid id, string name) => new(id, name);
    
    public void Update(string name) => Name = name;
}