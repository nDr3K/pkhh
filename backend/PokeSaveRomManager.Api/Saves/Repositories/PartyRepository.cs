using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly PokemonDbContext _context;

        public PartyRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task DeleteParty(int partyId)
        {
            var party = await _context.SaveTeams.FindAsync(partyId);
            if (party != null)
            {
                var partyMemebers = await _context.SaveTeamMembers
                    .Where(m => m.PartyId == partyId)
                    .ToListAsync();
                _context.SaveTeamMembers.RemoveRange(partyMemebers);
                await _context.SaveChangesAsync();
            }
        }
    }
}
