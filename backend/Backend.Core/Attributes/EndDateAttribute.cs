using Backend.Core.DTOs;
using System.ComponentModel.DataAnnotations;

public class EndDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dto = (CarItemRequestDto)validationContext.ObjectInstance;

        var expectedEndDate = dto.StartDate.AddDays(dto.DurationYear * 365);

        if (dto.EndDate.Date != expectedEndDate.Date)
        {
            return new ValidationResult($"Ngày kết thúc không hợp lệ.");
        }

        return ValidationResult.Success;
    }
}