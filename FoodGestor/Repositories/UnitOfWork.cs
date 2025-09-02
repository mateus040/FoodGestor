using FoodGestor.Context;
using FoodGestor.Repositories.Categories;
using FoodGestor.Repositories.Ingredients;

namespace FoodGestor.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository? _categoryRepository;
        private IIngredientRepository? _ingredientRepository;

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

        public IIngredientRepository IngredientRepository
        { 
            get
            {
                return _ingredientRepository ??= new IngredientRepository(_context);
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
