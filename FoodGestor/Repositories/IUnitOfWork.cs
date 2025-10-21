using FoodGestor.Repositories.Categories;
using FoodGestor.Repositories.Ingredients;

namespace FoodGestor.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        Task CommitAsync();
    }
}
