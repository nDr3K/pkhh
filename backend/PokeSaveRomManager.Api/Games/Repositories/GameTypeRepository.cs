using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public class GameTypeRepository : IGameTypeRepository
    {
        private readonly PokemonDbContext _context;

        public GameTypeRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameType>> GetAllAsync()
        {
            return await _context.GameTypes
                .Include(t => t.Game)
                .Include(t => t.Type)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GameType> GetByIdAsync(int id)
        {
            return await _context.GameTypes
                .Include(t => t.Game)
                .Include(t => t.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<GameType> AddAsync(GameType gameType)
        {
            _context.GameTypes.Add(gameType);
            await SaveChangesAsync();
            return await GetByIdAsync(gameType.Id);
        }

        public async Task UpdateAsync(GameType gameType)
        {
            _context.GameTypes.Update(gameType);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gameType = await _context.GameTypes.FindAsync(id);
            if (gameType != null)
            {
                _context.GameTypes.Remove(gameType);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GameTypes.AnyAsync(t => t.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
