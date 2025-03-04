namespace Organizations.Core.Entities;

public class OrganizationMember
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid UserId { get; private set; }
    public OrganizationRole Role { get; private set; }

    private OrganizationMember(Guid organizationId, Guid userId, OrganizationRole role)
    {
        Id = Guid.NewGuid();
        OrganizationId = organizationId;
        UserId = userId;
        Role = role;
    }

    public static OrganizationMember CreateOwner(Guid organizationId, Guid userId) 
        => new(organizationId, userId, OrganizationRole.Owner);
    
    public static OrganizationMember CreateMember(Guid organizationId, Guid userId, OrganizationRole role) 
        => new(organizationId, userId, role);
    
    public void UpdateRole(OrganizationRole role) => Role = role;
}