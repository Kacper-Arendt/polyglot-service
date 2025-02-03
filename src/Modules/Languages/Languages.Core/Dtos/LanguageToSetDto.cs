using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Languages.Core.Dtos;

public class LanguageToSetDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get;  set; }
    
    [StringLength(10, MinimumLength = 2)]
    public string Code { get;  set; }
    
    [Required]
    [ValidGuid]
    public Guid ProjectId { get; set; }
}