using System.ComponentModel.DataAnnotations;
using Organizations.Core.Entities;
using Shared.Infrastructure.Attributes;

namespace Organizations.Core.Dtos;

public class OrganizationMemberToSetDto
{
    [Required]
    [ValidGuid]
    public Guid UserId { get; set; }
    
    [Required]
    [ValidGuid]
    public Guid OrganizationId { get; set; }

    [Required]
    public OrganizationRole Role { get; set; } = OrganizationRole.Viewer;
}