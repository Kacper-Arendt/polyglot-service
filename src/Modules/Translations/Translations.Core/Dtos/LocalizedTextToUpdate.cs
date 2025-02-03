using System.ComponentModel.DataAnnotations;
using Shared.Infrastructure.Attributes;

namespace Translations.Core.Dtos;

public class LocalizedTextToUpdate
{
    [Required]
    public string Value { get;  set; }
}