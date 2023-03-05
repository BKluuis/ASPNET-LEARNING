using System.ComponentModel.DataAnnotations;

namespace e_CommerceUsingModelsAndValidation.CustomValidators
{
    public class MinimunYearAttribute : ValidationAttribute
    {
        public int Year { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "The minimum year for {0} should be {1}";

        public MinimunYearAttribute() { }
        public MinimunYearAttribute(int year)
        {
            Year = year;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime date = Convert.ToDateTime(value);
                if(date.Year < Year)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, validationContext.DisplayName, Year));
                } 
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
