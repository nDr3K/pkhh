using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
