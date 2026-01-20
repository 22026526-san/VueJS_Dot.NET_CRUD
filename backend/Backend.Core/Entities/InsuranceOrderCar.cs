namespace Backend.Core.Entities
{
    public class InsuranceOrderCar
    {
        public Guid CarItemId { get; set; } 
        
        // Liên kết với đơn hàng
        public Guid OrderId { get; set; }

        // --- Thông tin kỹ thuật xe ---
        public required string UsagePurpose { get; set; }
        public required string VehicleType { get; set; }
        public int SeatCount { get; set; }
        public decimal PayloadCapacity { get; set; } 

        // --- Thông tin thời hạn ---
        public int DurationYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeeOrder { get; set; } 

        // --- Thông tin định danh xe ---
        public string? RegistrationImageUrl { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? ChassisNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? LicensePlate { get; set; }
        public bool HasLicensePlate { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}