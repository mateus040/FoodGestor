using FoodGestor.Repositories.Categories;

namespace FoodGestor.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        Task CommitAsync();
    }
}
