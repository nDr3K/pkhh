using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            return await _context.Pokemon
                .AsNoTracking()
                .Include(p => p.Game)
                .ToListAsync();
        }

        public async Task<Pokemon> GetByIdAsync(int id)
        {
            return await _context.Pokemon
                .AsNoTracking()
                .Include(p => p.Game)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pokemon> CreateAsync(Pokemon pokemon)
        {
            _context.Pokemon.Add(pokemon);
            await SaveChangesAsync();
            return await GetByIdAsync(pokemon.Id);
        }

        public async Task<IEnumerable<Pokemon>> AddRangeAsync(IEnumerable<Pokemon> pokemons)
        {
            var trackedPokemons = pokemons.ToList();
            _context.Pokemon.AddRange(trackedPokemons);
            await SaveChangesAsync();
            return trackedPokemons;
        }

        public async Task UpdateAsync(Pokemon pokemon)
        {
            _context.Pokemon.Update(pokemon);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pokemon = await GetByIdAsync(id);
            if (pokemon != null)
            {
                _context.Pokemon.Remove(pokemon);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Pokemon>> GetByGameIdAsync(int gameId)
        {
            return await _context.Pokemon
                .AsNoTracking()
                .Include(p => p.Game) // Might not be needed
                .Where(p => p.GameId == gameId)
                .ToListAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Pokemon.AnyAsync(p => p.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
