using System.Linq.Expressions;

namespace FoodGestor.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
