using FoodGestor.Context;
using FoodGestor.Entitites;
using FoodGestor.Models;
using FoodGestor.Pagination;

namespace FoodGestor.Repositories.Ingredients
{
    public class IngredientRepository : Repository<IngredientEntity>, IIngredientRepository
    {
        public IngredientRepository(FoodGestorContext context) : base(context)
        {
        }

        public async Task<PagedList<IngredientModel>> GetIngredientsAsync(IngredientsParameters ingredientsParameters)
        {
            var ingredients = await GetAllAsync();

            var ingredientModel = ingredients
                .OrderBy(i => i.Name)
                .Select(i => new IngredientModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    UnitMeasure = i.UnitMeasure,
                    Quantity = i.Quantity,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt,
                })
                .AsQueryable();

            var result = PagedList<IngredientModel>.ToPagedList(ingredientModel, ingredientsParameters.Page, ingredientsParameters.PageSize);

            return result;
        }
    }
}
