using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Languages.Core.Dtos;

public class LanguageToUpdateDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    
    [StringLength(10, MinimumLength = 2)]
    public string Code { get;  set; }
}