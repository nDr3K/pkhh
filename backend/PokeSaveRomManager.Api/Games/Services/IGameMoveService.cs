using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameMoveService
    {
        Task<IEnumerable<GameMoveDto>> GetAllAsync(int gameId);
        Task<GameMoveDto> GetByIdAsync(int gameId, int id);
        Task<GameMoveDto> AddAsync(int gameId, GameMoveCreateDto gameMove);
        Task AddRangeAsync(int gameId, IEnumerable<GameMoveCreateDto> gameMoves);
        Task UpdateAsync(int gameId, int id, GameMoveUpdateDto gameMove);
        Task DeleteAsync(int gameId, int id);
        Task<bool> ExistsAsync(int gameId, int id);
    }
}
