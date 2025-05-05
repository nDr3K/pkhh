using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameTypeService
    {
        Task<IEnumerable<GameTypeDto>> GetAllAsync(int gameId);
        Task<GameTypeDto> GetByIdAsync(int gameId, int id);
        Task<GameTypeDto> AddAsync(int gameId, GameTypeCreateDto gameType);
        Task AddRangeAsync(int gameId, IEnumerable<GameTypeCreateDto> gameTypes);
        Task UpdateAsync(int gameId, int id, GameTypeUpdateDto gameType);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
