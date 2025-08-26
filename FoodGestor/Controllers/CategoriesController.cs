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
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListServiceResult<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListServiceResult<CategoryModel>>> Get([FromQuery] CategoriesParameters categoriesParameters)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(categoriesParameters);

            var result = new ListServiceResult<CategoryModel>(
                    categories.ToList(),
                    categories.CurrentPage,
                    categories.PageSize,
                    categories.TotalCount
                );

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModel>> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(c => c.Id == id);

            if (category is null)
            {
                return NotFound(new ServiceResult<CategoryModel>("Categoria não encontrada"));
            }

            var model = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            var result = new ServiceResult<CategoryModel>(model);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResult<CategoryModel>>> Post([FromBody] CategoryArgs categoryArgs)
        {
            new CategoryArgs.Validator().ValidateAndThrow(categoryArgs);

            var entity = new CategoryEntity
            {
                Name = categoryArgs.Name,
            };

            var createdEntity = _unitOfWork.CategoryRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            var model = new CategoryModel
            {
                Id = createdEntity.Id,
                Name = createdEntity.Name,
                CreatedAt = createdEntity.CreatedAt,
                UpdatedAt = createdEntity.UpdatedAt
            };

            var result = new ServiceResult<CategoryModel>(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResult<CategoryModel>>> Put(int id, [FromBody] CategoryArgs categoryArgs)
        {
            new CategoryArgs.Validator().ValidateAndThrow(categoryArgs);

            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(c => c.Id == id);

            if (entity is null)
            {
                return NotFound(new ServiceResult<CategoryModel>("Categoria não encontrada"));
            }

            entity.Name = categoryArgs.Name;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            var model = new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            var result = new ServiceResult<CategoryModel>(model);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult<CategoryModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(c => c.Id == id);

            if (entity is null)
            {
                return NotFound(new ServiceResult<CategoryModel>("Categoria não encontrada"));
            }

            _unitOfWork.CategoryRepository.Delete(entity);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
