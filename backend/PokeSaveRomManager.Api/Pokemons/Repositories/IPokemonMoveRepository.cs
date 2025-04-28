using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public interface IPokemonMoveRepository
    {
        Task<IEnumerable<PokemonMove>> GetAllAsync(int pokemonId);
        Task<PokemonMove> GetByIdAsync(int id);
        Task<PokemonMove> CreateAsync(PokemonMove move);
        Task UpdateAsync(PokemonMove move);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
    }
}
