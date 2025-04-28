using PokeSaveRomManager.Api.Pokemons.DTOs;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public interface IPokemonFormService
    {
        Task<PokemonFormDto> GetByIdAsync(int id);
        Task<IEnumerable<PokemonFormDto>> GetAllAsync(int pokemonId);
        Task<PokemonFormDto> CreateAsync(int pokemonId, PokemonFormCreateDto form);
        Task UpdateAsync(int id, int pokemonId, PokemonFormUpdateDto form);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
