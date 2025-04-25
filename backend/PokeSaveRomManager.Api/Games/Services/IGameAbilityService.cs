using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameAbilityService
    {
        Task<IEnumerable<GameAbilityDto>> GetAllAsync();
        Task<GameAbilityDto> GetByIdAsync(int id);
        Task<GameAbilityDto> AddAsync(GameAbilityCreateDto gameAbility);
        Task UpdateAsync(int id, GameAbilityUpdateDto gameAbility);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
