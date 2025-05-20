using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public class PokemonInstanceRepository : IPokemonInstanceRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonInstanceRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task DeletePokemons(int saveId)
        {
            var pokemons = await _context.PokemonInstances
                .Where(p => p.SaveId == saveId)
                .ToListAsync();
            _context.PokemonInstances.RemoveRange(pokemons);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PokemonInstance>> SavePokemonInstances(IEnumerable<PokemonInstance> pokemonInstances)
        {
            var trackedPokemonInstances = pokemonInstances.ToList();
            _context.PokemonInstances.AddRange(trackedPokemonInstances);
            await _context.SaveChangesAsync();
            return trackedPokemonInstances;
        }
    }
}
