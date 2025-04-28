using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public class PokemonMoveRepository : IPokemonMoveRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonMoveRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PokemonMove>> GetAllAsync(int pokemonId)
        {
            return await _context.PokemonMove
                .Include(pm => pm.Pokemon)
                .Include(pm => pm.GameMove)
                .Include(pm => pm.Method)
                .Where(pm => pm.PokemonId == pokemonId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<PokemonMove> GetByIdAsync(int id)
        {
            return await _context.PokemonMove
                .Include(pm => pm.Pokemon)
                .Include(pm => pm.GameMove)
                .Include(pm => pm.Method)
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.Id == id);
        }
        public async Task<PokemonMove> CreateAsync(PokemonMove move)
        {
            _context.PokemonMove.Add(move);
            await SaveChangesAsync();
            return move;
        }
        public async Task UpdateAsync(PokemonMove move)
        {
            _context.PokemonMove.Update(move);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var move = await GetByIdAsync(id);
            if (move != null)
            {
                _context.PokemonMove.Remove(move);
                await SaveChangesAsync();
            }
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.PokemonMove.AnyAsync(pm => pm.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
