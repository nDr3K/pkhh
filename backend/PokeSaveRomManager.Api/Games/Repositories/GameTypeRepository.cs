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

        public async Task<IEnumerable<GameType>> GetAllAsync(int gameId)
        {
            return await _context.GameTypes
                .Include(t => t.Type)
                .Where(t => t.GameId == gameId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GameType> GetByIdAsync(int gameId, int id)
        {
            return await _context.GameTypes
                .Include(t => t.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.GameId == gameId && t.TypeId == id);
        }

        public async Task<GameType> AddAsync(GameType gameType)
        {
            _context.GameTypes.Add(gameType);
            await SaveChangesAsync();
            return await _context.GameTypes.FirstOrDefaultAsync(t => t.Id == gameType.Id);
        }

        public async Task AddRangeAsync(IEnumerable<GameType> gameTypes)
        {
            _context.GameTypes.AddRange(gameTypes);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(GameType gameType)
        {
            _context.GameTypes.Update(gameType);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int gameId, int id)
        {
            var gameType = await GetByIdAsync(gameId, id);
            if (gameType != null)
            {
                _context.GameTypes.Remove(gameType);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            return await _context.GameTypes.AnyAsync(t => t.TypeId == id && t.GameId == gameId);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
