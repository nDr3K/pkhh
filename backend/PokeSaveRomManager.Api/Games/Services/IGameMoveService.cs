using PokeSaveRomManager.Api.Games.DTOs;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameMoveService
    {
        Task<IEnumerable<GameMoveDto>> GetAllAsync();
        Task<GameMoveDto> GetByIdAsync(int id);
        Task<GameMoveDto> AddAsync(GameMoveCreateDto gameMove);
        Task UpdateAsync(int id, GameMoveUpdateDto gameMove);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
