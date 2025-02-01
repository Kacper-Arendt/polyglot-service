using System.ComponentModel.DataAnnotations;

namespace Shared.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ValidGuidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is Guid guid && guid == Guid.Empty)
        {
            return new ValidationResult("The Guid cannot be the default value.");
        }
        
        return ValidationResult.Success;
    }
}