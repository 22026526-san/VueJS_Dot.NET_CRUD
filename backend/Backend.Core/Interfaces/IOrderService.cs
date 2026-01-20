using Backend.Core.DTOs;

namespace Backend.Core.Interfaces
{
    public interface IOrderService
    {
        // Hàm xử lý logic lưu đơn hàng và trả về OrderId
        Task<Guid> CreateOrder(OrderRequestDto dto);

        // Hàm lấy dữ liệu chi tiết đơn hàng kèm danh sách xe
        Task<OrderDetailResponseDto?> GetOrderDetails(Guid id);

        // Hàm lấy tất cả đơn hàng
        Task<IEnumerable<OrderResponseDto>> GetAllOrders();
    }
}