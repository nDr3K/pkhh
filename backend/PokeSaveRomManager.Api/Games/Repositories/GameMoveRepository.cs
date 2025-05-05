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

        public async Task<IEnumerable<GameMove>> GetAllAsync(int gameId)
        {
            return await _context.GameMoves
                .Include(gm => gm.Move)
                .Where(gm => gm.GameId == gameId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GameMove> GetByIdAsync(int gameId, int id)
        {
            return await _context.GameMoves
                .Include(gm => gm.Move)
                .AsNoTracking()
                .FirstOrDefaultAsync(gm => gm.MoveId == id && gm.GameId == gameId);
        }

        public async Task<GameMove> AddAsync(GameMove gameMove)
        {
            _context.GameMoves.Add(gameMove);
            await SaveChangesAsync();
            return await _context.GameMoves.FirstOrDefaultAsync(gm => gm.Id == gameMove.Id);
        }

        public async Task AddRangeAsync(IEnumerable<GameMove> gameMoves)
        {
            await _context.GameMoves.AddRangeAsync(gameMoves);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(GameMove gameMove)
        {
            _context.GameMoves.Update(gameMove);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int gameId, int id)
        {
            var gameMove = await GetByIdAsync(gameId, id);
            if (gameMove != null)
            {
                _context.GameMoves.Remove(gameMove);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            return await _context.GameMoves.AnyAsync(gm => gm.MoveId == id && gm.GameId == gameId);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
