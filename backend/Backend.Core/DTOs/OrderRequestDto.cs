using System.ComponentModel.DataAnnotations;

namespace Backend.Core.DTOs
{
    public class CarItemRequestDto
    {
        [Required]
        public string UsagePurpose { get; set; } = string.Empty;

        [Required]
        public string VehicleType { get; set; } = string.Empty;

        [Range(2, 45, ErrorMessage = "Số chỗ ngồi phải từ 2 đến 45.")]
        public int SeatCount { get; set; }

        [Range(100, 10000, ErrorMessage = "Trọng tải phải từ 100 đến 10000 kg.")]
        public decimal PayloadCapacity { get; set; }

        [Range(1, 10, ErrorMessage = "Thời hạn bảo hiểm từ 1 đến 10 năm.")]
        public int DurationYear { get; set; }

        [Required]
        [FutureDate] 
        public DateTime StartDate { get; set; }

        [Required]
        [EndDate] 
        public DateTime EndDate { get; set; }

        public decimal FeeOrder { get; set; }

        [MaxLength(20, ErrorMessage = "Số khung tối đa 20 ký tự.")]
        public string? ChassisNumber { get; set; }

        [MaxLength(20, ErrorMessage = "Số máy tối đa 20 ký tự.")]
        public string? EngineNumber { get; set; }

        [LicensePlate] 
        public string? LicensePlate { get; set; }

        public bool HasLicensePlate { get; set; }
        public string? RegistrationImageUrl { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
    }

    public class OrderRequestDto
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm.")]
        public Guid ProductId { get; set; }

        public decimal TotalAmount { get; set; }

        [MinLength(1, ErrorMessage = "Phải có ít nhất 1 xe.")]
        [MaxLength(3, ErrorMessage = "Danh sách chỉ chứa tối đa 3 xe.")]
        public List<CarItemRequestDto> Cars { get; set; } = [];
    }
}