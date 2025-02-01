using System.ComponentModel.DataAnnotations;

namespace Projects.Core.Dtos;

public class ProjectToUpdateDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(300)]
    public string Description { get; set; }
}