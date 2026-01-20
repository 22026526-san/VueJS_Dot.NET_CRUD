using Backend.Core.DTOs;
using Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto request)
        {
            var result = await _orderService.CreateOrder(request);
            return Ok(new { OrderId = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderDetails(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }
    }
}