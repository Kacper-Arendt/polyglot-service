using System.ComponentModel.DataAnnotations;

namespace Organizations.Core.Dtos;

public class OrganizationToSetDto
{
    [Required]
    [StringLength(100), MinLength(3)]
    public string Name { get; set; }
}