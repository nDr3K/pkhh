using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameAbilityRepository
    {
        Task<IEnumerable<GameAbility>> GetAllAsync(int gameId);
        Task<GameAbility> GetByIdAsync(int gameId, int id);
        Task<GameAbility> AddAsync(GameAbility gameAbility);
        Task UpdateAsync(GameAbility gameAbility);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
