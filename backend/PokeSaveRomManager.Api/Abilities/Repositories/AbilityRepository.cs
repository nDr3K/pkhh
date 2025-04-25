using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Abilities.Repositories
{
    public class AbilityRepository : IAbilityRepository
    {
        private readonly PokemonDbContext _context;

        public AbilityRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ability>> GetAllAsync()
        {
            return await _context.Abilities
                .Include(a => a.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ability> GetByIdAsync(int id)
        {
            return await _context.Abilities
                .Include(a => a.Name)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Ability> CreateAsync(Ability ability)
        {
            _context.Abilities.Add(ability);
            await _context.SaveChangesAsync();
            return ability;
        }

        public async Task UpdateAsync(Ability ability)
        {
            _context.Abilities.Update(ability);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ability = await GetByIdAsync(id);
            if (ability != null)
            {
                _context.Abilities.Remove(ability);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Abilities
                .AsNoTracking()
                .AnyAsync(a => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
