using PokeSaveRomManager.Api.Categories.DTOs;
using PokeSaveRomManager.Api.Categories.Mapper;
using PokeSaveRomManager.Api.Categories.Repositories;

namespace PokeSaveRomManager.Api.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all categories");
                var categories = await _categoryRepository.GetAllAsync();
                return categories.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all categories");
                throw;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving category with ID: {Id}", id);
                var category = await _categoryRepository.GetByIdAsync(id);
                return category.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving category with ID: {Id}", id);
                throw;
            }
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating new category: {CategoryName}", categoryCreateDto.Name);
                var category = categoryCreateDto.ToEntity();
                var createdCategory = await _categoryRepository.CreateAsync(category);
                return createdCategory.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category: {CategoryName}", categoryCreateDto.Name);
                throw;
            }
        }
        public async Task UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating category with ID: {Id}", id);
                var category = await _categoryRepository.GetByIdAsync(id);
                category.UpdateFromDto(categoryUpdateDto);
                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID: {Id}", id);
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting category with ID: {Id}", id);
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking if category with ID: {Id} exists", id);
                return await _categoryRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if category with ID: {Id} exists", id);
                throw;
            }
        }
    }
}
