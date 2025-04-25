using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameAbilityRepository
    {
        Task<IEnumerable<GameAbility>> GetAllAsync();
        Task<GameAbility> GetByIdAsync(int id);
        Task<GameAbility> AddAsync(GameAbility gameAbility);
        Task UpdateAsync(GameAbility gameAbility);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
