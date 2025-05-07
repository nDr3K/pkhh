using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Moves.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly PokemonDbContext _context;

        public MoveRepository(PokemonDbContext context)
        {
            _context = context;
        }

        // Move
        public async Task<IEnumerable<Move>> GetAllAsync()
        {
            return await _context.Moves
                .Include(m => m.Name)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Move> GetByIdAsync(int id)
        {
            return await _context.Moves
                .Include(m => m.Name)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Move> CreateAsync(Move move)
        {
            _context.Moves.Add(move);
            await SaveChangesAsync();
            return await GetByIdAsync(move.Id);
        }

        public async Task<IEnumerable<Move>> AddRangeAsync(IEnumerable<Move> moves)
        {
            await _context.Moves.AddRangeAsync(moves);
            await SaveChangesAsync();
            return moves;
        }

        public async Task UpdateAsync(Move move)
        {
            _context.Moves.Update(move);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var move = await GetByIdAsync(id);
            if (move != null)
            {
                _context.Moves.Remove(move);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Moves.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Move>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Moves
                .Include(m => m.Name)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .AsNoTracking()
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Move>> GetByTypeIdAndCategoryIdAsync(int typeId, int categoryId)
        {
            return await _context.Moves
                .Include(m => m.Name)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .AsNoTracking()
                .Where(m => m.TypeId == typeId && m.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Move>> GetByTypeIdAsync(int typeId)
        {
            return await _context.Moves
                .Include(m => m.Name)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .AsNoTracking()
                .Where(m => m.TypeId == typeId)
                .ToListAsync();
        }

        // MoveName
        public async Task<IEnumerable<MoveName>> GetAllNamesAsync()
        {
            return await _context.MoveNames
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MoveName> GetNameByIdAsync(int id)
        {
            return await _context.MoveNames
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MoveName> CreateNameAsync(MoveName moveName)
        {
            _context.MoveNames.Add(moveName);
            await SaveChangesAsync();
            return moveName;
        }

        public async Task<IEnumerable<MoveName>> AddNameRangeAsync(IEnumerable<MoveName> moveNames)
        {
            await _context.MoveNames.AddRangeAsync(moveNames);
            await SaveChangesAsync();
            return moveNames;
        }

        public async Task UpdateNameAsync(MoveName moveName)
        {
            _context.MoveNames.Update(moveName);
            await SaveChangesAsync();
        }

        public async Task DeleteNameAsync(int id)
        {
            var moveName = await GetNameByIdAsync(id);
            if (moveName != null)
            {
                _context.MoveNames.Remove(moveName);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsNameAsync(int id)
        {
            return await _context.MoveNames.AnyAsync(m => m.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
