using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class TranslationKeyToUpdate
{
    private string _name;

    [Required]
    [StringLength(255)]
    public string Name
    {
        get => _name;
        set => _name = value.Replace(" ", string.Empty);
    }  
}