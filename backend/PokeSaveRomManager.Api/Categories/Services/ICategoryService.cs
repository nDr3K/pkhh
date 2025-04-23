using PokeSaveRomManager.Api.Categories.DTOs;

namespace PokeSaveRomManager.Api.Categories.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
