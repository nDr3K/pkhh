using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Categories.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PokemonDbContext _context;

        public CategoryRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
