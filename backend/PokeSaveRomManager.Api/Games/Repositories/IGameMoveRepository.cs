using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameMoveRepository
    {
        Task<IEnumerable<GameMove>> GetAllAsync();
        Task<GameMove> GetByIdAsync(int id);
        Task<GameMove> AddAsync(GameMove gameMove);
        Task UpdateAsync(GameMove gameMove);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
