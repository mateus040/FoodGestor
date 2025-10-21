using FoodGestor.Entitites;
using FoodGestor.Models;
using FoodGestor.Pagination;

namespace FoodGestor.Repositories.Ingredients
{
    public interface IIngredientRepository : IRepository<IngredientEntity>
    {
        Task<PagedList<IngredientModel>> GetIngredientsAsync(IngredientsParameters ingredientsParameters);
    }
}
