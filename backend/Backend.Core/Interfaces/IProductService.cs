using Backend.Core.DTOs;

namespace Backend.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProducts();
        Task<ProductResponseDto> CreateProduct(ProductCreateDto productDto);
    }
}