using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class TranslationKeyToSet
{
    private string _name;

    [Required]
    [StringLength(255)]
    public string Name
    {
        get => _name;
        set => _name = value.Replace(" ", string.Empty);
    }  
    
    [Required]
    [ValidGuid]
    public Guid ProjectId { get; set; }
}