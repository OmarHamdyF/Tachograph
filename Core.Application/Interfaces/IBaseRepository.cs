using System.Linq.Expressions;

namespace Core.Application.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        void Add(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Load(T entity, Expression<Func<T, object>> propertyExpression);
    }

}
