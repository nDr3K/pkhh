using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(int id);
        Task<Game> GetByIdWithRelatedEntitiesAsync(int id);
        Task<Game> AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Game>> GetByGenerationAsync(int generation);
        Task<IEnumerable<Game>> GetByRegionAsync(string region);
        Task<IEnumerable<Game>> GetOfficialAsync();
    }
}
