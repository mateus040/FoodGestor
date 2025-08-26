using FoodGestor.Context;
using FoodGestor.Repositories.Categories;

namespace FoodGestor.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository? _categoryRepository;

        public FoodGestorContext _context;

        public UnitOfWork(FoodGestorContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
