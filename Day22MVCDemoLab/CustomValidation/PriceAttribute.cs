using System.ComponentModel.DataAnnotations;

namespace MVCDemoLabpart1.CustomValidation
{
    // Static Validation
    public class PriceAttribute: ValidationAttribute
    {
        public static string MyErrorMessage { get; set; }
        public static ValidationResult ValidatePrice(decimal valueAmount, ValidationContext context)
        {
            if (valueAmount < 500) return new ValidationResult(MyErrorMessage);
            return ValidationResult.Success;
        }
    }
}
