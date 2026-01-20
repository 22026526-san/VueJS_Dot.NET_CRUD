namespace Backend.Core.Entities
{
    public class InsuranceProduct
    {
        public Guid ProductId { get; set; } 
        public required string ProductName { get; set; }
        public required string ProductLogo { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxCoverage { get; set; }
        public int? MaxDuration { get; set; }
    }
}