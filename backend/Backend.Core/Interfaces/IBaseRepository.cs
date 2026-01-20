namespace Backend.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(object id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(object id);
    }
}