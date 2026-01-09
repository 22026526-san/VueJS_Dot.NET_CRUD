using System; 

namespace Backend.Core.Entities
{
    public class InsuranceProduct
    {
        public Guid Id { get; set; } 
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}