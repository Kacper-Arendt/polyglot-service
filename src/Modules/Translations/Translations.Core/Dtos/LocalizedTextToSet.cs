using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class LocalizedTextToSet
{
    [Required]
    public string Value { get;  set; }
    
    [Required]
    [ValidGuid]
    public Guid TranslationKeyId { get; set; }
    
    [Required]
    [ValidGuid]
    public Guid LanguageId { get;  set; }

}