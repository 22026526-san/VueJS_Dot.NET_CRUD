using System.ComponentModel.DataAnnotations;

namespace Backend.Core.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public required string ProductName { get; set; }

        [Required(ErrorMessage = "Logo sản phẩm không được để trống")]
        public required string ProductLogo { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxCoverage { get; set; }
        public int? MaxDuration { get; set; }
    }
}