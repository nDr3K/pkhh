using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public interface IPokemonFormRepository
    {
        Task<PokemonForm> GetByIdAsync(int id);
        Task<IEnumerable<PokemonForm>> GetAllAsync(int pokemonId);
        Task<PokemonForm> CreateAsync(PokemonForm form);
        Task UpdateAsync(PokemonForm form);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
