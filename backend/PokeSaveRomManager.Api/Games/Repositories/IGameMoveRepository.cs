using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameMoveRepository
    {
        Task<IEnumerable<GameMove>> GetAllAsync(int gameId);
        Task<GameMove> GetByIdAsync(int gameId, int id);
        Task<GameMove> AddAsync(GameMove gameMove);
        Task AddRangeAsync(IEnumerable<GameMove> gameMoves);
        Task UpdateAsync(GameMove gameMove);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
