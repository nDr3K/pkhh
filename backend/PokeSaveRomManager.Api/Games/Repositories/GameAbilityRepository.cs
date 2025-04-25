using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Repositories
{
    public class GameAbilityRepository : IGameAbilityRepository
    {
        private readonly PokemonDbContext _context;

        public GameAbilityRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameAbility>> GetAllAsync(int gameId)
        {
            return await _context.GameAbilities
                .Include(ga => ga.Ability)
                .Where(ga => ga.GameId == gameId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<GameAbility> GetByIdAsync(int gameId, int id)
        {
            return await _context.GameAbilities
                .Include(ga => ga.Ability)
                .AsNoTracking()
                .FirstOrDefaultAsync(ga => ga.AbilityId == id && ga.GameId == gameId);
        }
        public async Task<GameAbility> AddAsync(GameAbility gameAbility)
        {
            _context.GameAbilities.Add(gameAbility);
            await SaveChangesAsync();
            return await _context.GameAbilities.FirstOrDefaultAsync(ga => ga.Id == gameAbility.Id);
        }
        public async Task UpdateAsync(GameAbility gameAbility)
        {
            _context.GameAbilities.Update(gameAbility);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(int gameId, int id)
        {
            var gameAbility = await GetByIdAsync(gameId, id);
            if (gameAbility != null)
            {
                _context.GameAbilities.Remove(gameAbility);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            return await _context.GameAbilities.AnyAsync(ga => ga.AbilityId == id && ga.GameId == gameId);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
