using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Projects.Core.Dtos;

public class ProjectLanguageToUpdateDto
{
    [Required]
    [ValidGuid]
    public Guid LanguageId { get; set; }
}