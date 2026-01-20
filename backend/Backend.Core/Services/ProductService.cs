using Backend.Core.DTOs;
using Backend.Core.Entities;
using Backend.Core.Interfaces;

namespace Backend.Core.Services
{
    public class ProductService(IProductRepository repository) : IProductService
    {
        private readonly IProductRepository _repository = repository;

        public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
        {
            var products = await _repository.GetAll();
            
            // Mapping từ Entity sang ResponseDto
            return products.Select(static p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductLogo = p.ProductLogo,
                MaxCoverage = p.MaxCoverage,
                MaxDuration = p.MaxDuration,
                MinPrice = p.MinPrice
            });
        }

        public async Task<ProductResponseDto> CreateProduct(ProductCreateDto dto)
        {
            // 1. Mapping từ CreateDto sang Entity để lưu xuống DB
            var entity = new InsuranceProduct
            {
                ProductId = Guid.NewGuid(),
                ProductName = dto.ProductName,
                ProductLogo = dto.ProductLogo ,
                MinPrice = dto.MinPrice,
                MaxCoverage = dto.MaxCoverage,
                MaxDuration = dto.MaxDuration
            };

            await _repository.Add(entity);

            return new ProductResponseDto
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductLogo = entity.ProductLogo,
            };
        }
    }
}