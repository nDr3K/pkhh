using PokeSaveRomManager.Api.Pokemons.DTOs;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetByIdAsync(int id);
        Task<IEnumerable<PokemonDto>> GetAllAsync();
        Task<PokemonDto> CreateAsync(PokemonCreateDto pokemon);
        Task<IEnumerable<PokemonDto>> AddRangeAsync(IEnumerable<PokemonCreateDto> pokemons);
        Task UpdateAsync(int id, PokemonUpdateDto pokemon);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
        Task<IEnumerable<PokemonDto>> GetByGameIdAsync(int gameId);
        Task<IEnumerable<PokemonFormDto>> GetForGameByIds(int gameId, IEnumerable<int?> internalIds);
    }
}
