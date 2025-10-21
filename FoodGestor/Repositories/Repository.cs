using FoodGestor.Context;
using FoodGestor.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodGestor.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected readonly FoodGestorContext _context;

        public Repository(FoodGestorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            if (entity is BaseDateTrackedEntity trackedEntity)
            {
                trackedEntity.CreatedAt = DateTime.UtcNow;
                trackedEntity.UpdatedAt = DateTime.UtcNow;
            }

            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Update(T entity)
        {
            if (entity is BaseDateTrackedEntity trackedEntity)
            {
                trackedEntity.UpdatedAt = DateTime.UtcNow;
            }

            _context.Set<T>().Update(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            var prop = typeof(T).GetProperty("DeletedAt");

            if (prop != null && prop.PropertyType == typeof(DateTime))
            {
                prop.SetValue(entity, DateTime.UtcNow);

                if (entity is BaseDateTrackedEntity trackedEntity)
                {
                    trackedEntity.DeletedAt = DateTime.UtcNow;
                }

                _context.Set<T>().Update(entity);
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }
}
