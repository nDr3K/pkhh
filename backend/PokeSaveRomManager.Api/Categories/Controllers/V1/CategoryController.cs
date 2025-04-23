using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Categories.DTOs;
using PokeSaveRomManager.Api.Categories.Services;

namespace PokeSaveRomManager.Api.Categories.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : Controller
    {
        public readonly ICategoryService _categoryService;
        public readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            _logger.LogInformation("Getting category with id: {Id}", id);
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning("Category with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating new category: {CategoryName}", category.Name);
            var createdCategory = await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryUpdateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating category with id: {Id}", id);
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
            {
                _logger.LogWarning("Category with id: {Id} not found", id);
                return NotFound();
            }

            await _categoryService.UpdateAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation("Deleting category with id: {Id}", id);
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
            {
                _logger.LogWarning("Category with id: {Id} not found", id);
                return NotFound();
            }

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
