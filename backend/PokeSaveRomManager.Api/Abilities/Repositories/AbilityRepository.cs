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

        //Ability
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

        //AbilityName
        public async Task<IEnumerable<AbilityName>> GetAllNamesAsync()
        {
            return await _context.AbilityNames
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AbilityName> GetNameByIdAsync(int id)
        {
            return await _context.AbilityNames
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AbilityName> CreateNameAsync(AbilityName abilityName)
        {
            _context.AbilityNames.Add(abilityName);
            await _context.SaveChangesAsync();
            return abilityName;
        }

        public async Task UpdateNameAsync(AbilityName abilityName)
        {
            _context.AbilityNames.Update(abilityName);
            await SaveChangesAsync();
        }

        public async Task DeleteNameAsync(int id)
        {
            var abilityName = await GetNameByIdAsync(id);
            if (abilityName != null)
            {
                _context.AbilityNames.Remove(abilityName);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsNameAsync(int id)
        {
            return await _context.AbilityNames
                .AsNoTracking()
                .AnyAsync(a => a.Id == id);
        }
    }
}
