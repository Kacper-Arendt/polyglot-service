namespace Organizations.Core.Entities;

public class OrganizationMember
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid UserId { get; private set; }
    public OrganizationRole Role { get; private set; }
    public string Email { get; private set; }

    private OrganizationMember(Guid organizationId, Guid userId, OrganizationRole role, string email)
    {
        Id = Guid.NewGuid();
        OrganizationId = organizationId;
        UserId = userId;
        Role = role;
        Email = email;
    }

    public static OrganizationMember CreateMember(Guid organizationId, Guid userId, OrganizationRole role, string email) 
        => new(organizationId, userId, role, email);
    
    public void UpdateRole(OrganizationRole role) => Role = role;
    
    public void UpdateEmail(string email) => Email = email;
}