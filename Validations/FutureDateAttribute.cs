using System.ComponentModel.DataAnnotations;

namespace webapp_travel_agency.Validations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        var date = Convert.ToDateTime(value);

        if (date <= DateTime.Today)
        {
            return new ValidationResult(ErrorMessage);
        }
        
        return ValidationResult.Success;
    }
}