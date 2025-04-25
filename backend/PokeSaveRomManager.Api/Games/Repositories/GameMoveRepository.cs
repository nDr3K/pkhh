using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public class GameMoveRepository : IGameMoveRepository
    {
        private readonly PokemonDbContext _context;

        public GameMoveRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameMove>> GetAllAsync()
        {
            return await _context.GameMoves
                .Include(gm => gm.Move)
                .Include(gm => gm.Game)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GameMove> GetByIdAsync(int id)
        {
            return await _context.GameMoves
                .Include(gm => gm.Move)
                .Include(gm => gm.Game)
                .AsNoTracking()
                .FirstOrDefaultAsync(gm => gm.Id == id);
        }

        public async Task<GameMove> AddAsync(GameMove gameMove)
        {
            _context.GameMoves.Add(gameMove);
            await SaveChangesAsync();
            return await GetByIdAsync(gameMove.Id);
        }

        public async Task UpdateAsync(GameMove gameMove)
        {
            _context.GameMoves.Update(gameMove);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gameMove = await GetByIdAsync(id);
            if (gameMove != null)
            {
                _context.GameMoves.Remove(gameMove);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GameMoves.AnyAsync(gm => gm.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
