using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly PokemonDbContext _context;

        public GameRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game> GetByIdWithRelatedEntitiesAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Pokemon)
                .Include(g => g.Teams)
                .Include(g => g.Boxes)
                .Include(g => g.Moves)
                .Include(g => g.Types)
                .Include(g => g.Abilities)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game> AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await SaveChangesAsync();
            return game;
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetByGenerationAsync(int generation)
        {
            return await _context.Games
                .Where(g => g.Generation == generation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetByRegionAsync(string region)
        {
            return await _context.Games
                .Where(g => g.Region.ToLower() == region.ToLower())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetOfficialAsync()
        {
            return await _context.Games
                .Where(g => g.Official)
                .AsNoTracking()
                .ToListAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
