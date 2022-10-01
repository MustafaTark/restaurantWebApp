
using System.Linq.Expressions;

namespace restaurantWebApp.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<T> CreateAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpadteAsync(int id, T obj);
        Task<bool?> DeleteAsync(int id);
    }

}
