using System.Linq.Expressions;

namespace AutorizacionDePagos.Api.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        Task<T> GetByIdAsync(Guid id);        
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<List<T>> GetListByConditionAsync(Expression<Func<T, bool>> condition);
        Task AddRangeAsync(List<T> entities);
        Task<List<T>> GetAllListAsync();
    }
}
