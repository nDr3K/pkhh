using PokeSaveRomManager.Api.Items.DTOs;

namespace PokeSaveRomManager.Api.Items.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto> GetByIdAsync(int id);
        Task<ItemDto> CreateAsync(ItemCreateDto ItemCreateDto);
        Task UpdateAsync(int id, ItemUpdateDto ItemUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
