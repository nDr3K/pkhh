using PokeSaveRomManager.Data.Domain;
using PokeSaveRomManager.Data;
using Microsoft.EntityFrameworkCore;

namespace PokeSaveRomManager.Api.Items.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly PokemonDbContext _context;

        public ItemRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Item> CreateAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Items
                .AsNoTracking()
                .AnyAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Items
                .AsNoTracking()
                .AnyAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
