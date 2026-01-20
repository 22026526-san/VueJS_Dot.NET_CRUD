using Backend.Core.Entities;
using Backend.Core.Interfaces;
using Backend.Core.DTOs;

namespace Backend.Core.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<Guid> CreateOrder(OrderRequestDto dto)
        {
            var order = new InsuranceOrder
            {
                ProductId = dto.ProductId,
                TotalAmount = dto.TotalAmount,
                OrderCode = string.Concat("MISA", DateTime.Now.Ticks.ToString().AsSpan(8)),
                Status = "PENDING"
            };

            var cars = dto.Cars.Select(static c => new InsuranceOrderCar
            {
                UsagePurpose = c.UsagePurpose,
                VehicleType = c.VehicleType,
                SeatCount = c.SeatCount,
                PayloadCapacity = c.PayloadCapacity,
                DurationYear = c.DurationYear,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                FeeOrder = c.FeeOrder,
                RegistrationImageUrl = c.RegistrationImageUrl,
                Manufacturer = c.Manufacturer,
                Model = c.Model,
                ChassisNumber = c.ChassisNumber,
                EngineNumber = c.EngineNumber,
                LicensePlate = c.LicensePlate,
                HasLicensePlate = c.HasLicensePlate
            }).ToList();

            await _orderRepository.CreateOrderWithCars(order, cars);
            return order.OrderId;
        }

        public async Task<OrderDetailResponseDto?> GetOrderDetails(Guid id)
        {
            // Gọi xuống hàm GetOrderDetail mà bạn đã định nghĩa ở IOrderRepository
            return await _orderRepository.GetOrderDetail(id);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAll();

            return orders.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                OrderCode = o.OrderCode,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderDate = o.OrderDate
            });
        }
    }
}