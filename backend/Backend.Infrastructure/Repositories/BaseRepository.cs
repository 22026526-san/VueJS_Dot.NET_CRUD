using Backend.Core.Interfaces;
using Backend.Infrastructure.Data;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace Backend.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>(DapperContext context, string tableName, string primaryKey) : IBaseRepository<T> where T : class
    {
        protected readonly DapperContext _context = context;
        protected readonly string _tableName = tableName;
        protected readonly string _primaryKey = primaryKey;


        /// <summary>
        /// L?y theo t?t c?
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var query = $"SELECT * FROM {_tableName}";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(query);
        }

        /// <summary>
        /// L?y theo ID
        /// </summary>
        public virtual async Task<T?> GetById(object id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE {_primaryKey} = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        /// <summary>
        /// thêm m?i 
        /// </summary>
        public virtual async Task Add(T entity)
        {
            var properties = GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => ToSnakeCase(p.Name)));
            var paramNames = string.Join(", ", properties.Select(p => "@" + p.Name));
            var query = $"INSERT INTO {_tableName} ({columnNames}) VALUES ({paramNames})";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }


        /// <summary>
        /// C?p nh?t thông tin
        /// </summary>
        public virtual async Task Update(T entity)
        {
            var properties = GetProperties();
       
            var updateFields = string.Join(", ", properties.Select(p => $"{ToSnakeCase(p.Name)} = @{p.Name}"));

            var query = $"UPDATE {_tableName} SET {updateFields} WHERE {_primaryKey} = @{_primaryKey}";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        /// <summary>
        /// xóa theo ID
        /// </summary>
        public virtual async Task Delete(object id)
        {
            var query = $"DELETE FROM {_tableName} WHERE {_primaryKey} = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        /// <summary>
        /// L?y danh sách thu?c tính c?a Entity ?? map vào SQL
        /// </summary>
        private IEnumerable<PropertyInfo> GetProperties()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private string ToSnakeCase(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            return string.Concat(text.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()
            )).ToLower();
        }
    }
}