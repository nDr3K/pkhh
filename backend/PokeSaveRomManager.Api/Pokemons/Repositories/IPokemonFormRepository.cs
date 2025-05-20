using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public interface IPokemonFormRepository
    {
        Task<PokemonForm> GetByIdAsync(int id);
        Task<IEnumerable<PokemonForm>> GetAllAsync(int pokemonId);
        Task<PokemonForm> CreateAsync(PokemonForm form);
        Task<IEnumerable<PokemonForm>> AddRangeAsync(IEnumerable<PokemonForm> forms);
        Task UpdateAsync(PokemonForm form);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PokemonForm>> GetForGameByIds(int gameId, IEnumerable<int?> internalIds);
    }
}
