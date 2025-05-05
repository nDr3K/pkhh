using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameTypeRepository
    {
        Task<IEnumerable<GameType>> GetAllAsync(int gameId);
        Task<GameType> GetByIdAsync(int gameId, int id);
        Task<GameType> AddAsync(GameType gameType);
        Task AddRangeAsync(IEnumerable<GameType> gameTypes);
        Task UpdateAsync(GameType gameType);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
