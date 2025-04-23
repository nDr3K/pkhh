
using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using Type = PokeSaveRomManager.Data.Domain.Type;

namespace PokeSaveRomManager.Api.Types.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly PokemonDbContext _context;

        public TypeRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type>> GetAllAsync()
        {
            return await _context.Types
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Type> GetByIdAsync(int id)
        {
            return await _context.Types
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Type> AddAsync(Type type)
        {
            _context.Types.Add(type);
            await SaveChangesAsync();
            return type;
        }

        public async Task UpdateAsync(Type type)
        {
            _context.Types.Update(type);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var type = _context.Types.Find(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Types.AnyAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
