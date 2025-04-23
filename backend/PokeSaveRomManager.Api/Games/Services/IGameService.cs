using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<GameDto> GetGameByIdAsync(int id);
        Task<GameDto> AddGameAsync(GameCreateDto game);
        Task UpdateGameAsync(int id, GameUpdateDto game);
        Task DeleteGameAsync(int id);
        Task<bool> GameExistsAsync(int id);
        Task<IEnumerable<GameDto>> GetGamesByGenerationAsync(int generation);
        Task<IEnumerable<GameDto>> GetGamesByRegionAsync(string region);
        Task<IEnumerable<GameDto>> GetOfficialGamesAsync();
    }
}
