using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public interface IPokemonInstanceRepository
    {
        Task DeletePokemons(int saveId);
        Task<IEnumerable<PokemonInstance>> SavePokemonInstances(IEnumerable<PokemonInstance> pokemonInstances);
    }
}
