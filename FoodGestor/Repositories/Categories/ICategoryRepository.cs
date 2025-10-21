using FoodGestor.Entitites;
using FoodGestor.Models;
using FoodGestor.Pagination;

namespace FoodGestor.Repositories.Categories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<PagedList<CategoryModel>> GetCategoriesAsync(CategoriesParameters categoriesParameters);
    }
}
