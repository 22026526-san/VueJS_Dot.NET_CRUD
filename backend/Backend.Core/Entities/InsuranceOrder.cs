namespace Backend.Core.Entities
{
    public class InsuranceOrder
    {
        public Guid OrderId { get; set; } 
        public Guid ProductId { get; set; }
        public required string OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}