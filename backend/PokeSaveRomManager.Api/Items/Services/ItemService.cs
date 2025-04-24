using PokeSaveRomManager.Api.Items.DTOs;
using PokeSaveRomManager.Api.Items.Mapper;
using PokeSaveRomManager.Api.Items.Repositories;

namespace PokeSaveRomManager.Api.Items.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all items");
                var items = await _itemRepository.GetAllAsync();
                return items.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching items");
                throw;
            }
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching item with ID: {id}");
                var Item = await _itemRepository.GetByIdAsync(id);
                return Item.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching item with ID: {id}");
                throw;
            }
        }

        public async Task<ItemDto> CreateAsync(ItemCreateDto itemCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating new item");
                var item = itemCreateDto.ToEntity();
                var createdItem = await _itemRepository.CreateAsync(item);
                return createdItem.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating item");
                throw;
            }
        }

        public async Task UpdateAsync(int id, ItemUpdateDto itemUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating item with ID: {id}");
                var Item = await _itemRepository.GetByIdAsync(id);
                Item.UpdateFromDto(itemUpdateDto);
                await _itemRepository.UpdateAsync(Item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item with ID: {id}");
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting item with ID: {id}");
                await _itemRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item with ID: {id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking existence of item with ID: {id}");
                return await _itemRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of item with ID: {id}");
                throw;
            }
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            try
            {
                _logger.LogInformation($"Checking existence of item with name: {name}");
                return await _itemRepository.ExistsByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of item with name: {name}");
                throw;
            }
        }
    }
}
