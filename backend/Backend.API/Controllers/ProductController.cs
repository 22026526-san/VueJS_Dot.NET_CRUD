using Backend.Core.DTOs;
using Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productService.CreateProduct(productDto);
            return Ok(new { 
                Success = true, 
                NewId = result.ProductId 
            });
        }
    }
}