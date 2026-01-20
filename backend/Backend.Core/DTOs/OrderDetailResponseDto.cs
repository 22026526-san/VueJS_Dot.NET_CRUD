using Backend.Core.Entities;

namespace Backend.Core.DTOs
{
    public class OrderDetailResponseDto
    {
        public Guid OrderId { get; set; }
        public required string OrderCode { get; set; }
        public required string ProductLogo { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }

        public List<InsuranceOrderCar> Cars { get; set; } = [];
    }
}