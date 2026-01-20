using Backend.Core.Entities;
using Backend.Core.DTOs;

namespace Backend.Core.Interfaces
{
    public interface IOrderRepository : IBaseRepository<InsuranceOrder>
    {
        // Lưu cả Order và List Cars trong 1 Transaction
        Task CreateOrderWithCars(InsuranceOrder order, List<InsuranceOrderCar> cars);
        
        // Lấy Order và mảng Cars
        Task<OrderDetailResponseDto?> GetOrderDetail(Guid orderId);
    }
}