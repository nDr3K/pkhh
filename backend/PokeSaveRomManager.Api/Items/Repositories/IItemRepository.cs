using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Items.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(int id);
        Task<Item> CreateAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
