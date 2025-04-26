using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Moves.Repositories
{
    public class MoveLearningMethodRepository: IMoveLearningMethodRepository
    {
        private readonly PokemonDbContext _context;

        public MoveLearningMethodRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoveLearningMethod>> GetAllAsync()
        {
            return await _context.MoveLearningMethods
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MoveLearningMethod> GetByIdAsync(int id)
        {
            return await _context.MoveLearningMethods
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MoveLearningMethod> CreateAsync(MoveLearningMethod moveLearningMethod)
        {
            _context.MoveLearningMethods.Add(moveLearningMethod);
            await SaveChangesAsync();
            return moveLearningMethod;
        }
        public async Task UpdateAsync(MoveLearningMethod moveLearningMethod)
        {
            _context.MoveLearningMethods.Update(moveLearningMethod);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var moveLearningMethod = await GetByIdAsync(id);
            if (moveLearningMethod != null)
            {
                _context.MoveLearningMethods.Remove(moveLearningMethod);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.MoveLearningMethods.AnyAsync(m => m.Id == id);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
