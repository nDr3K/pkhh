using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public interface IPokemonAbilityRepository
    {
        Task<IEnumerable<PokemonAbility>> GetAllAsync(int pokemonId);
        Task<PokemonAbility> GetByIdAsync(int id);
        Task<PokemonAbility> CreateAsync(PokemonAbility dto);
        Task UpdateAsync(PokemonAbility dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
