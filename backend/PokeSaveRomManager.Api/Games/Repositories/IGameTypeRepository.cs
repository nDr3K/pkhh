using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameTypeRepository
    {
        Task<IEnumerable<GameType>> GetAllAsync();
        Task<GameType> GetByIdAsync(int id);
        Task<GameType> AddAsync(GameType gameType);
        Task UpdateAsync(GameType gameType);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
