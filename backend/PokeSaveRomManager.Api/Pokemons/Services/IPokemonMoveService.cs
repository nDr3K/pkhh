using PokeSaveRomManager.Api.Pokemons.DTOs;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public interface IPokemonMoveService
    {
        Task<IEnumerable<PokemonMoveDto>> GetAllAsync(int pokemonId);
        Task<PokemonMoveDto> GetByIdAsync(int id);
        Task<PokemonMoveDto> CreateAsync(int pokemonId, PokemonMoveCreateDto dto);
        Task UpdateAsync(int id, int pokemonId, PokemonMoveUpdateDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistASync(int id);
    }
}
