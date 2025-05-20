using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public interface IPokemonRepository
    {
        Task<Pokemon> GetByIdAsync(int id);
        Task<IEnumerable<Pokemon>> GetAllAsync();
        Task<Pokemon> CreateAsync(Pokemon pokemon);
        Task<IEnumerable<Pokemon>> AddRangeAsync(IEnumerable<Pokemon> pokemons);
        Task UpdateAsync(Pokemon pokemon);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
        Task<IEnumerable<Pokemon>> GetByGameIdAsync(int gameId);
        Task<IEnumerable<Pokemon>> GetForGameByIds(int gameId, IEnumerable<int?> internalIds);
    }
}
