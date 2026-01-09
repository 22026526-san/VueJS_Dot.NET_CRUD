using Backend.Core.Entities;
using Backend.Core.Interfaces;
using Backend.Infrastructure.Data;
using Dapper;

namespace Backend.Infrastructure.Repositories
{
    // Kế thừa BaseRepository để có sẵn hàm Get/Delete
    public class ProductRepository(DapperContext context) : BaseRepository<InsuranceProduct>(context, "insurance_product") , BaseRepository<InsuranceProduct>, IGenericRepository<InsuranceProduct>
    {
        public override async Task<int> AddAsync(InsuranceProduct entity)
        {
            var query = "INSERT INTO insurance_product (ProductName, Description, Price, CreatedAt) VALUES (@ProductName, @Description, @Price, NOW())";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, entity);
        }

        public override async Task<int> UpdateAsync(InsuranceProduct entity)
        {
            var query = "UPDATE insurance_product SET ProductName = @ProductName, Price = @Price WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, entity);
        }
    }
}