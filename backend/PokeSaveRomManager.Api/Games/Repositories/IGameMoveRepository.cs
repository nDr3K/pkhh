using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameMoveRepository
    {
        Task<IEnumerable<GameMove>> GetAllAsync(int gameId);
        Task<GameMove> GetByIdAsync(int gameId, int id);
        Task<GameMove> AddAsync(GameMove gameMove);
        Task UpdateAsync(GameMove gameMove);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
