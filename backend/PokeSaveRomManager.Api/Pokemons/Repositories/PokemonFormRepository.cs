using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Repositories
{
    public class PokemonFormRepository : IPokemonFormRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonFormRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<PokemonForm> GetByIdAsync(int id)
        {
            return await _context.PokemonForms
                .Include(pf => pf.Pokemon)
                .Include(pf => pf.Type1)
                .Include(pf => pf.Type2)
                .AsNoTracking()
                .FirstOrDefaultAsync(pf => pf.Id == id);
        }

        public async Task<IEnumerable<PokemonForm>> GetAllAsync(int pokemonId)
        {
            return await _context.PokemonForms
                .Include(pf => pf.Pokemon)
                .Include(pf => pf.Type1)
                .Include(pf => pf.Type2)
                .Where(pf => pf.PokemonId == pokemonId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PokemonForm> CreateAsync(PokemonForm form)
        {
            _context.PokemonForms.Add(form);
            await SaveChangesAsync();
            return await GetByIdAsync(form.Id);
        }

        public async Task<IEnumerable<PokemonForm>> AddRangeAsync(IEnumerable<PokemonForm> forms)
        {
            var trackedForms = forms.ToList();
            _context.PokemonForms.AddRange(trackedForms);
            await SaveChangesAsync();
            return trackedForms;
        }

        public async Task UpdateAsync(PokemonForm form)
        {
            _context.PokemonForms.Update(form);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var form = await GetByIdAsync(id);
            if (form != null)
            {
                _context.PokemonForms.Remove(form);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PokemonForms.AnyAsync(pf => pf.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
