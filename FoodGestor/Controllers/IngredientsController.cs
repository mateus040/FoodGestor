using FluentValidation;
using FoodGestor.Args;
using FoodGestor.Entitites;
using FoodGestor.Models;
using FoodGestor.Pagination;
using FoodGestor.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodGestor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListServiceResult<IngredientModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListServiceResult<IngredientModel>>> Get([FromQuery] IngredientsParameters ingredientsParameters)
        {
            var ingredients = await _unitOfWork.IngredientRepository.GetIngredientsAsync(ingredientsParameters);

            var result = new ListServiceResult<IngredientModel>(
                    ingredients.ToList(),
                    ingredients.CurrentPage,
                    ingredients.PageSize,
                    ingredients.TotalCount
                );

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IngredientModel>> GetById(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(i => i.Id == id);

            if (ingredient is null)
            {
                return NotFound(new ServiceResult<IngredientModel>("Ingrediente não encontado"));
            }

            var model = new IngredientModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                UnitMeasure = ingredient.UnitMeasure,
                Quantity = ingredient.Quantity,
                CreatedAt = ingredient.CreatedAt,
                UpdatedAt = ingredient.UpdatedAt,
            };

            var result = new ServiceResult<IngredientModel>(model);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResult<IngredientModel>>> Post([FromBody] IngredientArgs ingredientArgs)
        {
            new IngredientArgs.Validator().ValidateAndThrow(ingredientArgs);

            var entity = new IngredientEntity
            {
                Name = ingredientArgs.Name,
                UnitMeasure = ingredientArgs.UnitMeasure,
                Quantity = ingredientArgs.Quantity,
            };

            var createdEntity = _unitOfWork.IngredientRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            var model = new IngredientModel
            {
                Id = createdEntity.Id,
                Name = createdEntity.Name,
                UnitMeasure = createdEntity.UnitMeasure,
                Quantity = createdEntity.Quantity,
                CreatedAt = createdEntity.CreatedAt,
                UpdatedAt = createdEntity.UpdatedAt,
            };

            var result = new ServiceResult<IngredientModel>(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResult<IngredientModel>>> Put(int id, [FromBody] IngredientArgs ingredientArgs)
        {
            new IngredientArgs.Validator().ValidateAndThrow(ingredientArgs);

            var entity = await _unitOfWork.IngredientRepository.GetByIdAsync(i => i.Id == id);

            if (entity is null)
            {
                return NotFound(new ServiceResult<IngredientModel>("Ingrediente não encontrado."));
            }

            entity.Name = ingredientArgs.Name;
            entity.UnitMeasure = ingredientArgs.UnitMeasure;
            entity.Quantity = ingredientArgs.Quantity;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.IngredientRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            var model = new IngredientModel
            {
                Id = entity.Id,
                Name = entity.Name,
                UnitMeasure = entity.UnitMeasure,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };

            var result = new ServiceResult<IngredientModel>(model);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult<IngredientModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.IngredientRepository.GetByIdAsync(i => i.Id == id);

            if (entity is null)
            {
                return NotFound(new ServiceResult<IngredientModel>("Ingrediente não encontrado."));
            }

            _unitOfWork.IngredientRepository.Delete(entity);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
