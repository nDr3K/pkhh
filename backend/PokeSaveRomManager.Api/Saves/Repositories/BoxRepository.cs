using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly PokemonDbContext _context;
        public BoxRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<SaveBox> Create(SaveBox saveBox)
        {
            _context.SaveBoxes.Add(saveBox);
            await _context.SaveChangesAsync();
            return saveBox;
        }

        public async Task Delete(int boxId)
        {
            var boxSlots = await _context.SaveBoxSlots
                .Where(s => s.BoxId == boxId)
                .ToListAsync();

            _context.SaveBoxSlots.RemoveRange(boxSlots);
            await _context.SaveChangesAsync();
        }
    }
}
