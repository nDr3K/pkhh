using PokeSaveRomManager.Api.Pokemons.DTOs;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public interface IPokemonAbilityService
    {
        Task<IEnumerable<PokemonAbilityDto>> GetAllAsync(int pokemonId);
        Task<PokemonAbilityDto> GetByIdAsync(int id);
        Task<PokemonAbilityDto> CreateAsync(int pokemonId, PokemonAbilityCreateDto dto);
        Task UpdateAsync(int id, int pokemonId, PokemonAbilityUpdateDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
