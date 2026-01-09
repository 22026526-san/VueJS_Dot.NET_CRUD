using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Backend.Core.Interfaces;
using Backend.Infrastructure.Data;

namespace Backend.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>(DapperContext context, string tableName) : IGenericRepository<T> where T : class
    {
        protected readonly DapperContext _context = context;
        private readonly string _tableName = tableName;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_tableName}";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            // Dapper tự map tham số @Id (Guid) vào câu SQL
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var query = $"DELETE FROM {_tableName} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id });
        }

        public abstract Task<int> AddAsync(T entity);
        public abstract Task<int> UpdateAsync(T entity);
    }
}