using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Projects.Core.Dtos;

public class ProjectToCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(300)]
    public string Description { get; set; }
    
    [Required]
    [ValidGuid]
    public Guid BaseLanguage { get; set; }
}