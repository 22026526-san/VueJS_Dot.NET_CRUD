using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
    }
}
