using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameTypeService
    {
        Task<IEnumerable<GameTypeDto>> GetAllAsync();
        Task<GameTypeDto> GetByIdAsync(int id);
        Task<GameTypeDto> AddAsync(GameTypeCreateDto gameType);
        Task UpdateAsync(int id, GameTypeUpdateDto gameType);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
