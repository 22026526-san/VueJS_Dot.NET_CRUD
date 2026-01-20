using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class LicensePlateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var license = value as string;
        if (string.IsNullOrEmpty(license)) return ValidationResult.Success;

        if (license.Length > 10)
            return new ValidationResult("Biển số xe không được quá 10 ký tự.");

        // Regex: Chỉ cho phép A-Z, 0-9, dấu '-' và dấu '.'
        var regex = new Regex(@"^[A-Z0-9.-]+$");
        if (!regex.IsMatch(license))
        {
            return new ValidationResult("Biển số xe chỉ bao gồm chữ (A-Z), số (0-9), dấu '-' và dấu '.'");
        }

        return ValidationResult.Success;
    }
}