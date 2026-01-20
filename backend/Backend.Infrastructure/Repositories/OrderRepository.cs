using Backend.Core.Entities;
using Backend.Core.Interfaces;
using Backend.Core.DTOs;
using Backend.Infrastructure.Data;
using Dapper;
using System.Data;

namespace Backend.Infrastructure.Repositories
{
    public class OrderRepository(DapperContext context) : BaseRepository<InsuranceOrder>(context, "insurance_order", "order_id"), IOrderRepository
    {

        // API 1: Lưu đơn hàng Master-Detail dùng Transaction
        public async Task CreateOrderWithCars(InsuranceOrder order, List<InsuranceOrderCar> cars)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                if (order.OrderId == Guid.Empty)
                {
                    order.OrderId = Guid.NewGuid();
                }
                var sqlOrder = @"INSERT INTO insurance_order (order_id, product_id, order_code, total_amount, status) 
                                 VALUES (@OrderId, @ProductId, @OrderCode, @TotalAmount, @Status)";
                await connection.ExecuteAsync(sqlOrder, order, transaction);

                
                var sqlCar = @"INSERT INTO insurance_order_car (
                    car_item_id, order_id, usage_purpose, vehicle_type, seat_count, payload_capacity, 
                    duration_year, start_date, end_date, fee_order, registration_image_url, 
                    manufacturer, model, chassis_number, engine_number, license_plate, has_license_plate
                ) 
                VALUES (
                    @CarItemId, @OrderId, @UsagePurpose, @VehicleType, @SeatCount, @PayloadCapacity, 
                    @DurationYear, @StartDate, @EndDate, @FeeOrder, @RegistrationImageUrl, 
                    @Manufacturer, @Model, @ChassisNumber, @EngineNumber, @LicensePlate, @HasLicensePlate
                )";

                foreach (var car in cars)
                {
                    if (car.CarItemId == Guid.Empty)
                    {
                        car.CarItemId = Guid.NewGuid(); 
                    }
                    car.OrderId = order.OrderId; 
                    await connection.ExecuteAsync(sqlCar, car, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw; 
            }
        }

        // API 2: Lấy chi tiết đơn kèm mảng xe lồng nhau
        public async Task<OrderDetailResponseDto?> GetOrderDetail(Guid orderId)
        {
            var sql = @"SELECT 
                        o.order_id, o.order_code, o.total_amount, o.status, o.order_date,p.product_logo,
                        c.car_item_id, c.order_id, c.usage_purpose, c.vehicle_type, c.seat_count, 
                        c.payload_capacity, c.duration_year, c.start_date, c.end_date, c.fee_order, 
                        c.registration_image_url, c.manufacturer, c.model, c.chassis_number, 
                        c.engine_number, c.license_plate, c.has_license_plate
                        FROM insurance_order o
                        LEFT JOIN insurance_order_car c ON o.order_id = c.order_id
                        LefT JOIN insurance_product p ON o.product_id = p.product_id
                        WHERE o.order_id = @OrderId";

            using var connection = _context.CreateConnection();
            var orderDict = new Dictionary<Guid, OrderDetailResponseDto>();

            await connection.QueryAsync<OrderDetailResponseDto, InsuranceOrderCar, OrderDetailResponseDto>(
                sql,
                (order, car) =>
                {
                    if (!orderDict.TryGetValue(order.OrderId, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.Cars = []; 
                        orderDict.Add(currentOrder.OrderId, currentOrder);
                    }

                    if (car != null)
                    {
                        currentOrder.Cars.Add(car);
                    }
                    return currentOrder;
                },
                new { OrderId = orderId },
                splitOn: "car_item_id"
            );

            return orderDict.Values.FirstOrDefault();
        }
    }
}