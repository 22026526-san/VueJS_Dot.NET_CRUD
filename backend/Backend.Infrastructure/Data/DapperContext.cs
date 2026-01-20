using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Backend.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        // Constructor: Được gọi tự động khi ứng dụng chạy
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);
    }
}
