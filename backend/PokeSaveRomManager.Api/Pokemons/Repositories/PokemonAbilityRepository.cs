using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public class PokemonAbilityRepository : IPokemonAbilityRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonAbilityRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PokemonAbility>> GetAllAsync(int pokemonId)
        {
            return await _context.PokemonAbilities
                .Include(pa => pa.Pokemon)
                .Include(pa => pa.Ability)
                .ThenInclude(a => a.Name)
                .Where(pa => pa.PokemonId == pokemonId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PokemonAbility> GetByIdAsync(int id)
        {
            return await _context.PokemonAbilities
                .Include(pa => pa.Pokemon)
                .Include(pa => pa.Ability)
                .ThenInclude(a => a.Name)
                .AsNoTracking()
                .FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public async Task<PokemonAbility> CreateAsync(PokemonAbility dto)
        {
            _context.PokemonAbilities.Add(dto);
            await SaveChangesAsync();
            return await GetByIdAsync(dto.Id);
        }

        public async Task UpdateAsync(PokemonAbility dto)
        {
            _context.PokemonAbilities.Update(dto);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ability = await GetByIdAsync(id);
            _context.PokemonAbilities.Remove(ability);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PokemonAbilities.AnyAsync(pa => pa.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
