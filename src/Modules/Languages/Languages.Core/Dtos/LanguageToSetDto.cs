using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Languages.Core.Dtos;

public class LanguageToSetDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; private set; }
    
    [StringLength(10, MinimumLength = 2)]
    public string Code { get; private set; }
    
    [Required]
    [ValidGuid]
    public Guid ProjectId { get; private set; }
}