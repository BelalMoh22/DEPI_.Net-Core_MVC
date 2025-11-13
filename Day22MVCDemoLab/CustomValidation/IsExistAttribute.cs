using System.ComponentModel.DataAnnotations;

namespace MVCDemoLabpart1.CustomValidation
{
    // Attribute Prefix is used to define custom validation attributes in C#
    public class IsExistAttribute : ValidationAttribute
    {
        // Note : Custom Validation Works in Client and Server Side
        public string MyErrorMessage { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Check if the value is null or empty
            if(value == null) return null;

            // Get Data
            string data = value.ToString()!.Trim();
            // Create Instance From DBContext
            MVCDbContext _db = (MVCDbContext) validationContext.GetService(typeof(MVCDbContext)); //new MVCDbContext();

            // Check IsExist In Database or not
            Category categoryName = _db.Categories.FirstOrDefault(c => c.Name == data);

            if (categoryName != null)
            {
                return new ValidationResult(MyErrorMessage);
            }else
            {
                return ValidationResult.Success;
            }
        }
    }
}
