using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameAbilityService
    {
        Task<IEnumerable<GameAbilityDto>> GetAllAsync(int gameId);
        Task<GameAbilityDto> GetByIdAsync(int gameId, int id);
        Task<GameAbilityDto> AddAsync(int gameId, GameAbilityCreateDto gameAbility);
        Task UpdateAsync(int gameId, int id, GameAbilityUpdateDto gameAbility);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
