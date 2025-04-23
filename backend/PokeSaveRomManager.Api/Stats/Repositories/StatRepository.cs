using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Stats.Repositories
{
    public class StatRepository : IStatRepository
    {
        private readonly PokemonDbContext _context;

        public StatRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stat>> GetAllAsync()
        {
            return await _context.Stats
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Stat> GetByIdAsync(int id)
        {
            return await _context.Stats
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stat> AddAsync(Stat stat)
        {
            _context.Stats.Add(stat);
            await SaveChangesAsync();
            return stat;
        }

        public async Task UpdateAsync(Stat stat)
        {
            _context.Stats.Update(stat);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stat = await GetByIdAsync(id);
            if (stat != null)
            {
                _context.Stats.Remove(stat);
                await SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Stats.AnyAsync(s => s.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
