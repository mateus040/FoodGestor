using FoodGestor.Context;
using FoodGestor.Entitites;
using FoodGestor.Models;
using FoodGestor.Pagination;

namespace FoodGestor.Repositories.Categories
{
    public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(FoodGestorContext context) : base(context)
        {
        }

        public async Task<PagedList<CategoryModel>> GetCategoriesAsync(CategoriesParameters categoriesParameters)
        {
            var categories = await GetAllAsync();

            var categoriesModel = categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                })
                .AsQueryable();

            var result = PagedList<CategoryModel>.ToPagedList(categoriesModel, categoriesParameters.Page, categoriesParameters.PageSize);

            return result;
        }
    }
}
