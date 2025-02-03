using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class TranslationKeyToUpdate
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
}