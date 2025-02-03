using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class TranslationKeyToSet
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    [Required]
    [ValidGuid]
    public Guid ProjectId { get; set; }
}