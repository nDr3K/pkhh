using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Natures.Repositories
{
    public class NatureRepository : INatureRepository
    {
        private readonly PokemonDbContext _context;

        public NatureRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nature>> GetAllAsync()
        {
            return await _context.Natures
                .Include(n => n.IncreasedStat)
                .Include(n => n.DecreasedStat)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Nature> GetByIdAsync(int id)
        {
            return await _context.Natures
                .Include(n => n.IncreasedStat)
                .Include(n => n.DecreasedStat)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Nature> AddAsync(Nature nature)
        {
            _context.Natures.Add(nature);
            await SaveChangesAsync();
            return await GetByIdAsync(nature.Id);
        }

        public async Task UpdateAsync(Nature nature)
        {
            _context.Natures.Update(nature);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nature = await GetByIdAsync(id);
            if (nature != null)
            {
                _context.Natures.Remove(nature);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Natures.AnyAsync(n => n.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
