using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime.Date <= DateTime.Now.Date)
            {
                return new ValidationResult("Ngày bắt đầu phải lớn hơn ngày hiện tại.");
            }
        }
        return ValidationResult.Success;
    }
}