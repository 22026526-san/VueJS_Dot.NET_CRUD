namespace Backend.Core.DTOs
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string? OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}