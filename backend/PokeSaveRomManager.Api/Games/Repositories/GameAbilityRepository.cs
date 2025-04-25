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

        public async Task<IEnumerable<GameAbility>> GetAllAsync()
        {
            return await _context.GameAbilities
                .Include(ga => ga.Ability)
                .Include(ga => ga.Game)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<GameAbility> GetByIdAsync(int id)
        {
            return await _context.GameAbilities
                .FirstOrDefaultAsync(ga => ga.Id == id);
        }
        public async Task<GameAbility> AddAsync(GameAbility gameAbility)
        {
            _context.GameAbilities.Add(gameAbility);
            await SaveChangesAsync();
            return gameAbility;
        }
        public async Task UpdateAsync(GameAbility gameAbility)
        {
            _context.GameAbilities.Update(gameAbility);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var gameAbility = await GetByIdAsync(id);
            if (gameAbility != null)
            {
                _context.GameAbilities.Remove(gameAbility);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GameAbilities.AnyAsync(ga => ga.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
